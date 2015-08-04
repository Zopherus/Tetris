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
    public enum GameState { Menu, Play};
    public class Tetris : Game
    {
        public static int screenWidth;
        public static int screenHeight;

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

        public static SpriteFont spriteFont;

        public static KeyboardState keyboard;
        public static KeyboardState oldKeyboard;
        public static MouseState mouse;
        public static MouseState oldMouse;

        public static GameState gameState;

        public Tetris()
        {
            screenHeight = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Height;
            screenWidth = GraphicsAdapter.DefaultAdapter.CurrentDisplayMode.Width;
            graphics = new GraphicsDeviceManager(this)
            {
                IsFullScreen = true,
                PreferredBackBufferWidth =  screenWidth,
                PreferredBackBufferHeight = screenHeight
            };
            Content.RootDirectory = "Content";
            IsMouseVisible = true;
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
            gameState = GameState.Menu;
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
            BlueBlockTexture = Content.Load<Texture2D>("Sprites/blueBlock");
            CyanBlockTexture = Content.Load<Texture2D>("Sprites/cyanBlock");
            GreenBlockTexture = Content.Load<Texture2D>("Sprites/greenBlock");
            OrangeBlockTexture = Content.Load<Texture2D>("Sprites/orangeBlock");
            PurpleBlockTexture = Content.Load<Texture2D>("Sprites/purpleBlock");
            RedBlockTexture = Content.Load<Texture2D>("Sprites/redBlock");
            YellowBlockTexture = Content.Load<Texture2D>("Sprites/yellowBlock");
            spriteFont = Content.Load<SpriteFont>("SpriteFonts/SpriteFont");
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
            switch(gameState)
            {
                case GameState.Menu:
                    UpdateStates.UpdateMenu();
                    break;
                case GameState.Play:
                    UpdateStates.UpdatePlay();
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
            }
            spriteBatch.End();
            base.Draw(gameTime);
        }
    }
}
