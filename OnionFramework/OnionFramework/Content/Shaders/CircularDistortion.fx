#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float radius;
float aspectRatio;
float2 center;

Texture2D SpriteTexture;
sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
};

struct VertexShaderOutput
{
	float4 Position : SV_POSITION;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 texCoord = input.TextureCoordinates;
    float dx = texCoord.x - center.x, dy = texCoord.y - center.y;
    dx *= aspectRatio;
    
    float x = (dx*dx + dy*dy) - (radius*radius); 

    if(x < 0) {
        texCoord += float2(dx, dy) * x * 3;
    }
    return tex2D(SpriteTextureSampler, texCoord) * input.Color;
}

technique SpriteDrawing
{
	pass P0
	{
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};