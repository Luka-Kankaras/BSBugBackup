namespace OnionFramework.OnionFramework.ECS {
    public abstract class Component {
        #region Fields

        private Entity parent;

        #region Properties

        public Entity Parent {
            get => parent;
            set => parent = value;
        }

        #endregion
        
        #endregion

        protected Component(Entity parent) {
            this.parent = parent;
        }
    }
}