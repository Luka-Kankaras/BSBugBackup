using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Content;
using Microsoft.Xna.Framework.Graphics;

namespace OnionFramework.OnionFramework.ECS {
    public abstract class Entity {
        #region Fields

        protected string name;
        protected Transform transform;
        protected Entity parent;
        protected List<Component> components;
        protected List<Entity> children;

        #region Properties

        public string Name {
            get => name;
            set => name = value;
        }

        public Transform Transform {
            get => transform;
            set => transform = value;
        }

        public List<Component> Components => components;

        public Entity Parent => parent;

        public List<Entity> Children => children;

        #endregion
        
        #endregion

        
        protected Entity(Entity parent, string name) {
            this.name = name;
            transform = new Transform(Vector3.Zero, Vector3.One, Vector3.Zero);
            this.parent = parent;
            components = new List<Component>();
            children = new List<Entity>();
        }

        protected Entity(Entity parent, string name, Vector3 position, Vector3 rotation, Vector3 scale) {
            this.name = name;
            transform = new Transform(position, rotation, scale);
            this.parent = parent;
            components = new List<Component>();
            children = new List<Entity>();
        }

        
        #region Entity and Component handling methods

        public void Instantiate(Entity entity, Entity parent) {
            if(parent != null) parent.AddChild(entity);
            else Scene.ActiveScene.AddEntity(entity);
        }
        
        public void Destroy() {
            if(parent != null)
                parent.RemoveChild(this);
            else
                Scene.ActiveScene.RemoveEntity(this);
        }
        
        public void AddComponent(Component component) {
            foreach (Component current in components)
                if (current.GetType() == component.GetType()) return;
            
            components.Add(component);
        }

        public void RemoveComponent(Component component) {
            foreach (Component current in components)
                if (current == component) {
                    components.Remove(current);
                    return;
                }
        }

        public Component GetComponentOfType<T>() where T : Component {
            foreach (Component current in components)
                if (current.GetType() == typeof(T)) return current;

            return null;
        }
        
        public void RemoveComponentOfType<T>() where T : Component {
            foreach (Component current in components)
                if (current.GetType() == typeof(T)) {
                    components.Remove(current);
                    return;
                }
        }

        public void AddChild(Entity entity) {
            children.Add(entity);
        }

        public Entity GetChildOfName(string name) {
            foreach (Entity current in children)
                if (current.Name == name) return current;
            
            return null;
        }

        public List<Entity> GetChildrenOfType<T>() where T : Entity {
            List<Entity> childList = new List<Entity>();
            foreach (Entity current in children)
                if (current.GetType() == typeof(T)) childList.Add(current);

            return childList;
        }

        public void RemoveChildOfName(string name) {
            foreach (Entity current in children)
                if (current.Name == name) {
                    children.Remove(current);
                    return;
                }
        }

        public void RemoveChild(Entity entity) {
            foreach (Entity current in children)
                if (current == entity) {
                    children.Remove(current);
                    return;
                }
        }

        #endregion

        
        public virtual void Initialize() {
            foreach (Entity child in children)
                child.Initialize();
        }

        public virtual void LoadContent(ContentManager contentManager) {
            foreach (Entity child in children)
                child.LoadContent(contentManager);
        }

        public virtual void Update(GameTime gameTime) {
            foreach (Entity child in children)
                child.Update(gameTime);
        }

        public virtual void Draw() {
            foreach (Entity child in children)
                child.Draw();
        }
    }
}