using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Audio;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.GamerServices;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Media;

namespace Tetris
{
    public enum GameState { Menu, Play, Pause, Options, Highscore}
    //I is Cyan, O is Yellow, L is Orange, Z is Red, S is green, T is Purple, J is Blue
    public enum BlockType { I, J, L, O, S, T, Z};
    //Rotation states based off of http://vignette1.wikia.nocookie.net/tetrisconcept/images/3/3d/SRS-pieces.png/revision/latest?cb=20060626173148
    public enum RotationState { One, Two, Three, Four};
     

    public class TetrisGame : Game
    {
        //Grid sizes
        public const int boardWidth = 10;
        public const int boardHeight = 20;

        public static int screenWidth;
        public static int screenHeight;

        public static int gridSize;

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static Texture2D BlackTexture;
        public static Texture2D BlueBlockTexture;
        public static Texture2D CyanBlockTexture;
        public static Texture2D GreenBlockTexture;
        public static Texture2D OrangeBlockTexture;
        public static Texture2D PurpleBlockTexture;
        public static Texture2D RedBlockTexture;
        public static Texture2D YellowBlockTexture;
        public static Texture2D TransparentSquareTexture;
        public static Texture2D BackgroundTexture;

        public static Texture2D OptionsButtonPressedTexture;
        public static Texture2D OptionsButtonUnpressedTexture;
        public static Texture2D PlayButtonPressedTexture;
        public static Texture2D PlayButtonUnpressedTexture;
        public static Texture2D pokemonsunsetTexture;
        public static Texture2D QuitButtonPressedTexture;
        public static Texture2D QuitButtonUnpressedTexture;
        public static Texture2D HighscoreButtonPressedTexture;
        public static Texture2D HighscoreButtonUnpressedTexture;

        public static Texture2D IblockTexture;
        public static Texture2D JblockTexture;
        public static Texture2D LblockTexture;
        public static Texture2D OblockTexture;
        public static Texture2D SblockTexture;
        public static Texture2D TblockTexture;
        public static Texture2D ZblockTexture;

        public static Texture2D bordersquareTexture;

        public static SpriteFont PressStartFont;

        public static KeyboardState keyboard;
        public static KeyboardState oldKeyboard;
        public static MouseState mouse;
        public static MouseState oldMouse;

        public static Random random = new Random();

        public static GameState gameState;

        public static List<Board> GameBoards = new List<Board>();
        public static Board PlayerBoard;

        public TetrisGame()
        {
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            graphics = new GraphicsDeviceManager(this)
            {
                PreferredBackBufferWidth = screenWidth,
                PreferredBackBufferHeight = screenHeight,
                //IsFullScreen = true
            };
            IsMouseVisible = true;
            //2 extra gridsizes on top and 2 on bottom
            gridSize = (int)screenHeight / (4 + boardHeight);
            Content.RootDirectory = "Content";
        }

        /// <summary>
        /// Allows the game to perform any initialization it needs to before starting to run.
        /// This is where it can query for any required services and load any non-graphic
        /// related content.  Calling base.Initialize will enumerate through any components
        /// and initialize them as well.
        /// </summary>
        protected override void Initialize()
        {
            // TODO: Add your initialization logic here
            Start();
            base.Initialize();
        }

        /// <summary>
        /// LoadContent will be called once per game and is the place to load
        /// all of your content.
        /// </summary>
        protected override void LoadContent()
        {
            // Create a new SpriteBatch, which can be used to draw textures.
            spriteBatch = new SpriteBatch(GraphicsDevice);
            BlackTexture = Content.Load<Texture2D>("Sprites/Black");
            BlueBlockTexture = Content.Load<Texture2D>("Sprites/Blocks/blueBlock");
            CyanBlockTexture = Content.Load<Texture2D>("Sprites/Blocks/cyanBlock");
            GreenBlockTexture = Content.Load<Texture2D>("Sprites/Blocks/greenBlock");
            OrangeBlockTexture = Content.Load<Texture2D>("Sprites/Blocks/orangeBlock");
            PurpleBlockTexture = Content.Load<Texture2D>("Sprites/Blocks/purpleBlock");
            RedBlockTexture = Content.Load<Texture2D>("Sprites/Blocks/redBlock");
            YellowBlockTexture = Content.Load<Texture2D>("Sprites/Blocks/yellowBlock");
            TransparentSquareTexture = Content.Load<Texture2D>("Sprites/Transparent Square");
            BackgroundTexture = Content.Load<Texture2D>("Sprites/Background");

            OptionsButtonPressedTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/Options Button Pressed");
            OptionsButtonUnpressedTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/Options Button Unpressed");
            PlayButtonPressedTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/Play Button Pressed");
            PlayButtonUnpressedTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/Play Button Unpressed");
            pokemonsunsetTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/pokemon sunset");
            QuitButtonPressedTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/Quit Button Pressed");
            QuitButtonUnpressedTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/Quit Button Unpressed");
            HighscoreButtonPressedTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/HighscoreButtonPressed");
            HighscoreButtonUnpressedTexture = Content.Load<Texture2D>("Sprites/Menu Sprites/HighscoreButtonUnpressed");

            IblockTexture = Content.Load<Texture2D>("Sprites/Full Blocks/Iblock");
            JblockTexture = Content.Load<Texture2D>("Sprites/Full Blocks/Jblock");
            LblockTexture = Content.Load<Texture2D>("Sprites/Full Blocks/Lblock");
            OblockTexture = Content.Load<Texture2D>("Sprites/Full Blocks/Oblock");
            SblockTexture = Content.Load<Texture2D>("Sprites/Full Blocks/Sblock");
            TblockTexture = Content.Load<Texture2D>("Sprites/Full Blocks/Tblock");
            ZblockTexture = Content.Load<Texture2D>("Sprites/Full Blocks/Zblock");

            bordersquareTexture = Content.Load<Texture2D>("Sprites/Blocks/bordersquare");

            PressStartFont = Content.Load<SpriteFont>("Press Start 2P");
            // TODO: use this.Content to load your game content here
        }

        /// <summary>
        /// UnloadContent will be called once per game and is the place to unload
        /// all content.
        /// </summary>
        protected override void UnloadContent()
        {
            // TODO: Unload any non ContentManager content here
        }

        /// <summary>
        /// Allows the game to run logic such as updating the world,
        /// checking for collisions, gathering input, and playing audio.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Update(GameTime gameTime)
        {
            oldKeyboard = keyboard;
            oldMouse = mouse;
            keyboard = Keyboard.GetState();
            mouse = Mouse.GetState();
            if (keyboard.IsKeyDown(Keys.F1))
                this.Exit();
            switch(gameState)
            {
                case GameState.Menu:
                    UpdateStates.UpdateMenu();
                    break;
                case GameState.Play:
                    UpdateStates.UpdatePlay(gameTime);
                    break;
                case GameState.Pause:
                    UpdateStates.UpdatePause();
                    break;
                case GameState.Options:
                    UpdateStates.UpdateOptions();
                    break;
                case GameState.Highscore:
                    UpdateStates.UpdateHighscore();
                    break;
            }

            base.Update(gameTime);
        }

        /// <summary>
        /// This is called when the game should draw itself.
        /// </summary>
        /// <param name="gameTime">Provides a snapshot of timing values.</param>
        protected override void Draw(GameTime gameTime)
        {
            GraphicsDevice.Clear(Color.White);
            spriteBatch.Begin();
            switch (gameState)
            {
                case GameState.Menu:
                    DrawStates.DrawMenu();
                    break;
                case GameState.Play:
                    DrawStates.DrawPlay();
                    break;
                case GameState.Pause:
                    DrawStates.DrawPause();
                    break;
                case GameState.Options:
                    DrawStates.DrawOptions();
                    break;
                case GameState.Highscore:
                    DrawStates.DrawHighscore();
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void Start()
        {
            PlayerBoard = new Board(new Rectangle((screenWidth - (boardWidth * gridSize)) / 2,
                    (screenHeight - (boardHeight * gridSize)) / 2, boardWidth * gridSize, boardHeight * gridSize));
            GameBoards.Add(PlayerBoard);
            gameState = GameState.Menu;
            PlayerBoard.fillUpcomingPieces();
            PlayerBoard.CurrentPiece = new Piece(BlockType.I);
        }
    }
}
