package src.PeriferalListeners;

import org.lwjgl.glfw.GLFW;

public class MouseListener {

    private static MouseListener instance;
    private ButtonState[] buttonStates;
    private double positionX, positionY;

    private MouseListener() {
        positionX = 0;
        positionY = 0;
        buttonStates = new ButtonState[5];
    }

    public static MouseListener get() {
        if(instance == null) instance = new MouseListener();
        return MouseListener.instance;
    }

    public static void cursorPositionCallback(long window, double x, double y) {
        get().positionX = x;
        get().positionY = y;
    }

    public static void mouseButtonCallback(long window, int button, int action, int mods) {
        if(action == GLFW.GLFW_PRESS)
            get().buttonStates[button] = ButtonState.PRESSED;
        if(action == GLFW.GLFW_RELEASE)
            get().buttonStates[button] = ButtonState.RELEASED;
    }

    public static ButtonState getButtonState(int button) {
        return get().buttonStates[button];
    }
}
