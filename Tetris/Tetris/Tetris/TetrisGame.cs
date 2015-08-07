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
    public enum GameState { Menu, Play, Pause}
    //I is Cyan, O is Yellow, L is Orange, Z is Red, S is green, T is Purple, J is Blue
    public enum BlockType { I, J, L, O, S, T, Z};

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
        public static Texture2D TetrisBoardTexture;
        public static Texture2D TransparentSquareTexture;
        public static Texture2D BackgroundTexture;

        public static SpriteFont PressStartFont;

        public static KeyboardState keyboard;
        public static KeyboardState oldKeyboard;
        public static MouseState mouse;
        public static MouseState oldMouse;

        public static Random random = new Random();

        public static GameState gameState;

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
            //2 spaces on top and 2 on bottom
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
            TetrisBoardTexture = Content.Load<Texture2D>("Sprites/Tetris Board");
            TransparentSquareTexture = Content.Load<Texture2D>("Sprites/Transparent Square");
            BackgroundTexture = Content.Load<Texture2D>("Sprites/Background");

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
            if (keyboard.IsKeyDown(Keys.Escape))
                this.Exit();
            switch(gameState)
            {
                case GameState.Menu:
                    UpdateStates.UpdateMenu();
                    break;
                case GameState.Play:
                    UpdateStates.UpdatePlay();
                    break;
                case GameState.Pause:
                    UpdateStates.UpdatePause();
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
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }

        public static void Start()
        {
            PlayerBoard = new Board(new Rectangle((screenWidth - (boardWidth * gridSize)) / 2,
                    (screenHeight - (boardHeight * gridSize)) / 2, boardWidth * gridSize, boardHeight * gridSize));
            gameState = GameState.Play;
            /*for (int counter = 1; counter <= boardWidth; counter++ )
            {
                PlayerBoard.boardState[counter, 18] = new Block(BlockType.J, new Point(counter,18));
            }*/
            Array values = Enum.GetValues(typeof(BlockType));
            
            for (int x = 1; x <= boardWidth; x++ )
            {
                for (int y = 1; y <= boardHeight; y ++)
                {
                    BlockType randomBlockType = (BlockType)values.GetValue(random.Next(values.Length));
                    PlayerBoard.boardState[x, y] = new Block(randomBlockType, new Point(x, y));
                }
            }
           gameState = GameState.Play;
        }
    }
}
