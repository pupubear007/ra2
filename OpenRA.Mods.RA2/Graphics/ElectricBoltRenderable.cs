#region Copyright & License Information
/*
 * Copyright (c) The OpenRA Developers and Contributors
 * This file is part of OpenRA, which is free software. It is made
 * available to you under the terms of the GNU General Public License
 * as published by the Free Software Foundation, either version 3 of
 * the License, or (at your option) any later version. For more
 * information, see COPYING.
 */
#endregion

using System.Linq;
using OpenRA.Graphics;
using OpenRA.Primitives;

namespace OpenRA.Mods.RA2.Graphics
{
	public readonly struct ElectricBoltRenderable : IRenderable, IFinalizedRenderable
	{
		readonly WPos[] offsets;
		readonly WDist width;
		readonly Color color;

		public ElectricBoltRenderable(WPos[] offsets, int zOffset, WDist width, Color color)
		{
			this.offsets = offsets;
			ZOffset = zOffset;
			this.width = width;
			this.color = color;
		}

		public WPos Pos => new(offsets[0].X, offsets[0].Y, 0);
		public int ZOffset { get; }
		public bool IsDecoration => true;

		public IRenderable WithZOffset(int newOffset) { return new ElectricBoltRenderable(offsets, newOffset, width, color); }

		public IRenderable OffsetBy(in WVec vec)
		{
			var vec2 = vec;
			return new ElectricBoltRenderable(offsets.Select(offset => offset + vec2).ToArray(), ZOffset, width, color);
		}

		public IRenderable AsDecoration() { return this; }

		public IFinalizedRenderable PrepareRender(WorldRenderer wr) { return this; }
		public void Render(WorldRenderer wr)
		{
			var screenWidth = wr.ScreenVector(new WVec(width, WDist.Zero, WDist.Zero))[0];

			Game.Renderer.WorldRgbaColorRenderer.DrawLine(offsets.Select(offset => wr.Screen3DPosition(offset)), screenWidth, color, false);
		}

		public void RenderDebugGeometry(WorldRenderer wr) { }
		public Rectangle ScreenBounds(WorldRenderer wr) { return Rectangle.Empty; }
	}
}
