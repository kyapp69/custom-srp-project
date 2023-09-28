﻿using UnityEngine;
using UnityEngine.Experimental.Rendering.RenderGraphModule;
using UnityEngine.Rendering;
using UnityEngine.Rendering.RendererUtils;

public class GeometryPass
{
	static readonly ProfilingSampler
		samplerOpaque = new("Geometry Opaque"),
		samplerTransparent = new("Geometry Transparent");

	static readonly ShaderTagId[] shaderTagIds = {
		new("SRPDefaultUnlit"),
		new("CustomLit")
	};

	RendererListHandle list;

	void Render(RenderGraphContext context)
	{
		context.cmd.DrawRendererList(list);
		context.renderContext.ExecuteCommandBuffer(context.cmd);
		context.cmd.Clear();
	}

	public static void Record(
		RenderGraph renderGraph, Camera camera, CullingResults cullingResults,
		bool useLightsPerObject, int renderingLayerMask, bool opaque)
	{
		ProfilingSampler sampler = opaque ? samplerOpaque : samplerTransparent;
		using RenderGraphBuilder builder = renderGraph.AddRenderPass(
			sampler.name, out GeometryPass pass, sampler);
		PerObjectData lightsPerObjectFlags = useLightsPerObject ?
			PerObjectData.LightData | PerObjectData.LightIndices : PerObjectData.None;
		pass.list = builder.UseRendererList(renderGraph.CreateRendererList(
			new RendererListDesc(shaderTagIds, cullingResults, camera)
			{
				sortingCriteria = opaque ?
					SortingCriteria.CommonOpaque : SortingCriteria.CommonTransparent,
				rendererConfiguration =
					PerObjectData.ReflectionProbes |
					PerObjectData.Lightmaps |
					PerObjectData.ShadowMask |
					PerObjectData.LightProbe |
					PerObjectData.OcclusionProbe |
					PerObjectData.LightProbeProxyVolume |
					PerObjectData.OcclusionProbeProxyVolume |
					lightsPerObjectFlags,
				renderQueueRange = opaque ?
					RenderQueueRange.opaque : RenderQueueRange.transparent,
				renderingLayerMask = (uint)renderingLayerMask
			}));
		builder.SetRenderFunc<GeometryPass>((pass, context) => pass.Render(context));
	}
}
