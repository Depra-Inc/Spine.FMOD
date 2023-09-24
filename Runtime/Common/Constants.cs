// Copyright © 2023 Nikolay Melnikov. All rights reserved.
// SPDX-License-Identifier: Apache-2.0

namespace Depra.Spine.FMOD.Runtime.Common
{
	internal static class Constants
	{
		public const string FRAMEWORK_NAME = nameof(Depra);
		public const string MODULE_NAME = nameof(Spine) + DOT + nameof(FMOD);
		public const string MODULE_PATH = FRAMEWORK_NAME + SEPARATOR + MODULE_NAME;

		internal const string DOT = ".";
		internal const string SEPARATOR = "/";
		internal const int DEFAULT_ORDER = 52;
	}
}