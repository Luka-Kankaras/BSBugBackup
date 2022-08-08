package src.Util;

import src.Renderer.Shader;
import src.Renderer.Texture;

import java.io.File;
import java.util.HashMap;
import java.util.Map;

public class AssetManager {
    private static Map<String, Shader> shaderMap;
    private static Map<String, Texture> textureMap;

    static {
        shaderMap = new HashMap<>();
        textureMap = new HashMap<>();
    }

    public static Shader loadShader(String filepath) {
        File asset = new File(filepath);

        if(shaderMap.containsKey(asset.getAbsolutePath())) {
            return shaderMap.get(asset.getAbsolutePath());
        }
        else {
            Shader shader = new Shader(filepath);
            shader.compile();
            shaderMap.put(asset.getAbsolutePath(), shader);
            return shader;
        }
    }

    public static Texture loadTexture(String filepath) {
        File asset = new File(filepath);

        if(textureMap.containsKey(asset.getAbsolutePath())) {
            return textureMap.get(asset.getAbsolutePath());
        }
        else {
            Texture texture = new Texture(filepath);
            textureMap.put(asset.getAbsolutePath(), texture);
            return texture;
        }
    }
}
