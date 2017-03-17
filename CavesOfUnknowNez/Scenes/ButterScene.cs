using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Nez;
using Nez.Sprites;
using Nez.UI;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using CavesOfUnknowNez.Scenes;
using static Nez.Scene;

namespace CavesOfUnknowNez.Scenes
{
	public class ButterScene : Scene
	{


                
				public ButterScene()
				{
			
			

				}

		
		public override void initialize()
		{
			base.initialize();







            //var myScene = Scene.createWithDefaultRenderer(Color.Black);

            initiatePlayer();

            

			//scene = myScene;

			}

        private void initiatePlayer()
        {
            var playerTexture = content.Load<Texture2D>("Graphics/player");

            
            
            var playerOne = createEntity("Player1");

            playerOne.addComponent(new Sprite(playerTexture));

            //add movement simple mover
            playerOne.addComponent(new SimpleMover());
            playerOne.addComponent(new DamageComponent());

            playerOne.transform.position = new Vector2(200, 200);
        }
    }
}
