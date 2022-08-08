using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OnionFramework.OnionFramework.ECS {
    public abstract class Scene {
        #region Fields

        protected static Scene activeScene;
        protected List<Entity> entities = new List<Entity>();
        
        #region Properties

        public static Scene ActiveScene {
            get => activeScene;
            set => activeScene = value;
        }

        public List<Entity> Entities => entities;

        #endregion
        
        #endregion

        
        public void AddEntity(Entity entity) {
            entities.Add(entity);
        }

        public void RemoveEntity(Entity entity) {
            foreach (Entity current in entities)
                if (current == entity) {
                    entities.Remove(current);
                    return;
                }
        }

        
        public virtual void Initialize() {
            foreach (Entity current in entities)
                current.Initialize();
        } 
        
        public virtual void LoadContent(ContentManager contentManager) {
            foreach (Entity current in entities)
                current.LoadContent(contentManager);
        }
        
        public virtual void Update(GameTime gameTime) {
            foreach (Entity current in entities)
                current.Update(gameTime);
        }

        public virtual void Draw() {
            foreach (Entity current in entities)
                current.Draw();
        }
    }
}