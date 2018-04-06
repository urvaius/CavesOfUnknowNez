﻿using Microsoft.Xna.Framework;
using Nez.Sprites;
using Microsoft.Xna.Framework.Graphics;
using Nez.Textures;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Input;
using Nez.Tiled;
using Nez;
using CavesOfUnknowNez;

namespace CavesOfUnknowNez.Entities
{
	public class Player: Component, ITriggerListener, IUpdatable
	{

		public float moveSpeed = 150;
		public float gravity = 150;
		public float jumpHeight = 16 * 5;

		enum Animations
		{
			Walk,Run,Idle,Attack,Death,Falling,Hurt,Jumping

		}
		Sprite<Animations> _animation;

		TiledMapMover _mover;
		BoxCollider _boxCollider;
		TiledMapMover.CollisionState _collisionState = new TiledMapMover.CollisionState();
		Vector2 _velocity;
		VirtualButton _jumpInput;
		VirtualIntegerAxis _xAxisInput;

		public override void onAddedToEntity()
		{
			var texture = entity.scene.content.Load<Texture2D>(Content.Platformer.caveman);


			var subtextures = Subtexture.subtexturesFromAtlas(texture, 32, 32);

			_boxCollider = entity.getComponent<BoxCollider>();
			_mover = entity.getComponent<TiledMapMover>();
			_animation = entity.addComponent(new Sprite<Animations>(subtextures[0]));
			//extract the animations from the atlas they are setup in rows with 8 columns
			_animation.addAnimation(Animations.Walk, new SpriteAnimation(new List<Subtexture>()
			{
				subtextures[0],
				subtextures[1],
				subtextures[2],
				subtextures[3],
				subtextures[4],
				subtextures[5]}
				));

			_animation.addAnimation(Animations.Run, new SpriteAnimation(new List<Subtexture>()
			{
				subtextures[8+0],
				subtextures[8+1],
				subtextures[8+2],
				subtextures[8+3],
				subtextures[8+4],
				subtextures[8+5],
				subtextures[8+6]
			}));

			_animation.addAnimation(Animations.Idle, new SpriteAnimation(new List<Subtexture>()
			{
				subtextures[16]
			}));

			_animation.addAnimation(Animations.Attack, new SpriteAnimation(new List<Subtexture>()
			{
				subtextures[24+0],
				subtextures[24+1],
				subtextures[24+2],
				subtextures[24+3]
			}));

			_animation.addAnimation(Animations.Death, new SpriteAnimation(new List<Subtexture>()
			{
				subtextures[40+0],
				subtextures[40+1],
				subtextures[40+2],
				subtextures[40+3]
			}));

			_animation.addAnimation(Animations.Falling, new SpriteAnimation(new List<Subtexture>()
			{
				subtextures[48]
			}));

			_animation.addAnimation(Animations.Hurt, new SpriteAnimation(new List<Subtexture>()
			{
				subtextures[64],
				subtextures[64+1]
			}));

			_animation.addAnimation(Animations.Jumping, new SpriteAnimation(new List<Subtexture>()
			{
				subtextures[72+0],
				subtextures[72+1],
				subtextures[72+2],
				subtextures[72+3]
			}));

			setupInput();
			//base.onAddedToEntity();
		}

		public override void onRemovedFromEntity()
		{
			_jumpInput.deregister();
			_xAxisInput.deregister();
		}

		void setupInput()
		{
			// setup input for jumping. we will allow z on the keyboard or a on the gamepad
			_jumpInput = new VirtualButton();
			_jumpInput.nodes.Add(new Nez.VirtualButton.KeyboardKey(Keys.Space));
			_jumpInput.nodes.Add(new Nez.VirtualButton.GamePadButton(0, Buttons.A));

			// horizontal input from dpad, left stick or keyboard left/right
			_xAxisInput = new VirtualIntegerAxis();
			_xAxisInput.nodes.Add(new Nez.VirtualAxis.GamePadDpadLeftRight());
			_xAxisInput.nodes.Add(new Nez.VirtualAxis.GamePadLeftStickX());
			_xAxisInput.nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.Left, Keys.Right));
			_xAxisInput.nodes.Add(new Nez.VirtualAxis.KeyboardKeys(VirtualInput.OverlapBehavior.TakeNewer, Keys.A, Keys.D));


		}

		void IUpdatable.update()
		{
			// handle movement and animations
			var moveDir = new Vector2(_xAxisInput.value, 0);
			var animation = Animations.Idle;

			if(moveDir.X <0)
			{
				if (_collisionState.below)
					animation = Animations.Run;
				_animation.flipX = true;
				_velocity.X = -moveSpeed;
			}
			else if(moveDir.X >0)
			{
				if (_collisionState.below)
					animation = Animations.Run;
				_animation.flipX = false;
				_velocity.X = moveSpeed;
			}
			else
			{
				_velocity.X = 0;
				if (_collisionState.below)
					animation = Animations.Idle;
			}

			if (_collisionState.below && _jumpInput.isPressed)
			{
				animation = Animations.Jumping;
				_velocity.Y = -Mathf.sqrt(2f * jumpHeight * gravity);
			}


			if (!_collisionState.below && _velocity.Y > 0)
				animation = Animations.Falling;

			// apply gravity
			_velocity.Y += gravity * Time.deltaTime;

			// move
			_mover.move(_velocity * Time.deltaTime, _boxCollider, _collisionState);

			if (_collisionState.below)
				_velocity.Y = 0;

			if (!_animation.isAnimationPlaying(animation))
				_animation.play(animation);







		} 
	

		   void ITriggerListener.onTriggerEnter(Collider other, Collider self)
		{
			Debug.log("triggerEnter:{0}", other.entity.name);
		}

		void ITriggerListener.onTriggerExit(Collider other, Collider self)
		{
			Debug.log("triggerExit:{0}", other.entity.name);
		}
	}
}
