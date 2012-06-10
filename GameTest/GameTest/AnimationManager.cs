using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework;

namespace GameTest
{
    /// <summary>
    /// A class that handles animation looping and timing.
    /// 
    /// I hate this class name.
    /// </summary>
    public class AnimationManager
    {
        private Animation _currentAnimation;
        private int _frameMillisecondsLeft;
        private int _currentAnimationFrame;
        private Dictionary<string, Animation> _animations = new Dictionary<string, Animation>();

        public double AnimationTimeScale { get; set; }

        public Texture2D SpriteSheet { get; set; }

        public AnimationManager()
        {
            AnimationTimeScale = 1;
        }

        public void SetCurrentAnimation(string animationKey)
        {
            SetCurrentAnimation(animationKey, 0);
        }

        public void SetCurrentAnimation(string animationKey, int frameNumber)
        {
            _currentAnimation = _animations[animationKey];
            _currentAnimationFrame = frameNumber;
        }

        public void AdvanceCurrentAnimation()
        {
            if (_currentAnimation.Frames[_currentAnimationFrame].MillisecondsToDisplay == 0)
                return;

            _frameMillisecondsLeft -= (int)(MomolikeGame.GameTime.ElapsedGameTime.TotalMilliseconds * AnimationTimeScale);

            if (_frameMillisecondsLeft < 0)
            {
                _currentAnimationFrame++;

                if (_currentAnimationFrame == _currentAnimation.Frames.Length)
                    _currentAnimationFrame = _currentAnimation.LoopStartFrame;

                _frameMillisecondsLeft = _currentAnimation.Frames[_currentAnimationFrame].MillisecondsToDisplay;
            }
        }

        public void BlitCurrentAnimationFromSheet(SpriteBatch spriteBatch, Vector2 position)
        {
            spriteBatch.Draw(SpriteSheet, position, _currentAnimation.Frames[this._currentAnimationFrame].ClippingRectangle, Color.White);
        }

        public void AddAnimation(string animationKey, Animation a)
        {
            _animations.Add(animationKey, a);
        }
    }
}
