#if OPENGL
	#define SV_POSITION POSITION
	#define VS_SHADERMODEL vs_3_0
	#define PS_SHADERMODEL ps_3_0
#else
	#define VS_SHADERMODEL vs_4_0_level_9_1
	#define PS_SHADERMODEL ps_4_0_level_9_1
#endif

float4x4 view_projection;
float time;
float scale;
float period;

Texture2D SpriteTexture;
sampler2D SpriteTextureSampler = sampler_state
{
	Texture = <SpriteTexture>;
	//AddressU = Mirror;
};

struct VertexShaderOutput
{   
	float4 Position : POSITION0;
	float4 Color : COLOR0;
	float2 TextureCoordinates : TEXCOORD0;
};

VertexShaderOutput MainVS(float4 position : POSITION0, float4 color : COLOR0, float2 texCoords : TEXCOORD0) 
{
    VertexShaderOutput vo = (VertexShaderOutput)0;

    vo.Position = mul(position, view_projection);
    vo.Color = color;
    vo.TextureCoordinates = texCoords;
    
    return vo;
}

float4 MainPS(VertexShaderOutput input) : COLOR
{
    float2 distortedTexCoord = input.TextureCoordinates;
    distortedTexCoord.x = (distortedTexCoord.x - 0.5) * (1/scale) + 0.5;
    distortedTexCoord.x += float2((1 - scale) / 2 * (1 / scale) * sin(distortedTexCoord.y * 3.14159265 * period + time / 1000.0), 0);


    clip(distortedTexCoord.x);
    clip(1 - distortedTexCoord.x);
       
       
	float4 color = tex2D(SpriteTextureSampler, distortedTexCoord);
	return color;
}

technique SpriteDrawing
{
	pass P0
	{
	    VertexShader = compile VS_SHADERMODEL MainVS();
		PixelShader = compile PS_SHADERMODEL MainPS();
	}
};