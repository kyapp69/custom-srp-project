using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;

public class LightingPass
{
	static readonly ProfilingSampler sampler = new("Lighting");

	readonly Lighting lighting = new();

	void Render(RenderGraphContext context) => lighting.Render(context);

	public static ShadowTextures Record(
		RenderGraph renderGraph,
		CullingResults cullingResults, ShadowSettings shadowSettings,
		bool useLightsPerObject, int renderingLayerMask)
	{
		using RenderGraphBuilder builder = renderGraph.AddRenderPass(
			sampler.name, out LightingPass pass, sampler);
		pass.lighting.Setup(
			cullingResults, shadowSettings,
			useLightsPerObject, renderingLayerMask);
		builder.SetRenderFunc<LightingPass>(
			(pass, context) => pass.Render(context));
		builder.AllowPassCulling(false);
		return pass.lighting.GetShadowTextures(renderGraph, builder);
	}
}
