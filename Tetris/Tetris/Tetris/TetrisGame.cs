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
    public enum GameState {Menu, Play, Pause, Options, Highscore, EnterName}
    //I is Cyan, O is Yellow, L is Orange, Z is Red, S is green, T is Purple, J is Blue
    public enum BlockType {I, J, L, O, S, T, Z};
    //Rotation states based off of http://vignette1.wikia.nocookie.net/tetrisconcept/images/3/3d/SRS-pieces.png/revision/latest?cb=20060626173148
    public enum RotationState { One, Two, Three, Four};
    //Block positions as shown in http://imgur.com/7bjA0d9 sorry I can't draw for my life

    public class TetrisGame : Game
    {
        //Grid sizes
        public const int boardWidth = 10;
        public const int boardHeight = 20;

        public static int screenWidth;
        public static int screenHeight;

        public static int gridSize;

        public static string theme = "Food";

        public static GraphicsDeviceManager graphics;
        public static SpriteBatch spriteBatch;

        public static Texture2D BlackTexture;
        public static Texture2D JBlockTexture;
        public static Texture2D IBlockTexture;
        public static Texture2D SBlockTexture;
        public static Texture2D LBlockTexture;
        public static Texture2D TBlockTexture;
        public static Texture2D ZBlockTexture;
        public static Texture2D OBlockTexture;
        public static Texture2D TransparentSquareTexture;
        public static Texture2D ShadowBlockTexture;
        public static Texture2D BackgroundTexture;
        public static Texture2D TrashBlockTexture;

        public static Texture2D OptionsButtonPressedTexture;
        public static Texture2D OptionsButtonUnpressedTexture;
        public static Texture2D PlayButtonPressedTexture;
        public static Texture2D PlayButtonUnpressedTexture;
        public static Texture2D pokemonsunsetTexture;
        public static Texture2D QuitButtonPressedTexture;
        public static Texture2D QuitButtonUnpressedTexture;
        public static Texture2D HighscoreButtonPressedTexture;
        public static Texture2D HighscoreButtonUnpressedTexture;

        public static Texture2D IFullBlockTexture;
        public static Texture2D JFullBlockTexture;
        public static Texture2D LFullBlockTexture;
        public static Texture2D OFullBlockTexture;
        public static Texture2D SFullBlockTexture;
        public static Texture2D TFullBlockTexture;
        public static Texture2D ZFullBlockTexture;

        public static Texture2D OptionsCheckTexture;
        public static Texture2D OptionsUncheckTexture;

        public static Texture2D bordersquareTexture;
        public static Texture2D emptyTexture;

        public static SpriteFont PressStartFont;

        public static KeyboardState keyboard;
        public static KeyboardState oldKeyboard;
        public static MouseState mouse;
        public static MouseState oldMouse;

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
            BlackTexture = Content.Load<Texture2D>(theme + "/Black");
            JBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/JBlock");
            IBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/IBlock");
            SBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/SBlock");
            LBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/LBlock");
            TBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/TBlock");
            ZBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/ZBlock");
            OBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/OBlock");
            TransparentSquareTexture = Content.Load<Texture2D>(theme + "/Transparent Square");
            ShadowBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/shadowBlock");
            BackgroundTexture = Content.Load<Texture2D>(theme + "/Background");
            TrashBlockTexture = Content.Load<Texture2D>(theme + "/Blocks/trashblock");

            /* POKEMON THEME    : Pokeball = I Block
             *                  : Ultraball = L Block
             *                  : Masterball = J Block
             *                  : Premierball = Z Block
             *                  : Netball = T Block
             *                  : Duskball = O Block
             *                  : Great Ball = S Block
             */
                                
            /* EMOJI THEME      : Angel = I Block
             *                  : Asian = L Block
             *                  : Confused = J Block
             *                  : Lazy = Z Block
             *                  : Love = T Block
             *                  : Nerd = O Block
             *                  : Sparkly = S Block
             */ 

            /* FOOD THEME       : Watermelon = I Block
             *                  : Taco = L Block
             *                  : Cupcake = J Block
             *                  : Donut = Z Block
             *                  : Apple = T Block
             *                  : Pizza = O Block
             *                  : Cake = S Block
             */
             
            OptionsButtonPressedTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/Options Button Pressed");
            OptionsButtonUnpressedTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/Options Button Unpressed");
            PlayButtonPressedTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/Play Button Pressed");
            PlayButtonUnpressedTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/Play Button Unpressed");
            pokemonsunsetTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/pokemon sunset");
            QuitButtonPressedTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/Quit Button Pressed");
            QuitButtonUnpressedTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/Quit Button Unpressed");
            HighscoreButtonPressedTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/HighscoreButtonPressed");
            HighscoreButtonUnpressedTexture = Content.Load<Texture2D>(theme + "/Menu Sprites/HighscoreButtonUnpressed");

            IFullBlockTexture = Content.Load<Texture2D>(theme + "/Full Blocks/IFullBlock");
            JFullBlockTexture = Content.Load<Texture2D>(theme + "/Full Blocks/JFullBlock");
            LFullBlockTexture = Content.Load<Texture2D>(theme + "/Full Blocks/LFullBlock");
            OFullBlockTexture = Content.Load<Texture2D>(theme + "/Full Blocks/OFullBlock");
            SFullBlockTexture = Content.Load<Texture2D>(theme + "/Full Blocks/SFullBlock");
            TFullBlockTexture = Content.Load<Texture2D>(theme + "/Full Blocks/TFullBlock");
            ZFullBlockTexture = Content.Load<Texture2D>(theme + "/Full Blocks/ZFullBlock");

            OptionsCheckTexture = Content.Load<Texture2D>(theme + "/Blocks/OptionsCheck");
            OptionsUncheckTexture = Content.Load<Texture2D>(theme + "/Blocks/OptionsUncheck");

            bordersquareTexture = Content.Load<Texture2D>(theme + "/Blocks/bordersquare");
            emptyTexture = Content.Load<Texture2D>(theme + "/empty");

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
            //Used to exit the program
            if (keyboard.IsKeyDown(Keys.F1))
                this.Exit();
            //Run a switch on the gameState to see which update method to run
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
                case GameState.EnterName:
                    UpdateStates.UpdateEnterName();
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
            //Run a switch on the gameState to see which draw method to run
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
                case GameState.EnterName:
                    DrawStates.DrawEnterName();
                    break;
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        //Run to start/reset the game
        public static void Start()
        {
            PlayerBoard = new Board(new Rectangle((screenWidth - (boardWidth * gridSize)) / 2,
                    (screenHeight - (boardHeight * gridSize)) / 2, boardWidth * gridSize, boardHeight * gridSize));
            GameBoards.Add(PlayerBoard);
            gameState = GameState.Menu; 
            PlayerBoard.fillUpcomingPieces();
            PlayerBoard.changeCurrentPiece();
            Highscore.ReadFromFile();
        }
    }
}
