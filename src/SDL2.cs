#region License
/* SDL2# - C# Wrapper for SDL2
 *
 * Copyright (c) 2013 Ethan Lee.
 *
 * This software is provided 'as-is', without any express or implied warranty.
 * In no event will the authors be held liable for any damages arising from
 * the use of this software.
 *
 * Permission is granted to anyone to use this software for any purpose,
 * including commercial applications, and to alter it and redistribute it
 * freely, subject to the following restrictions:
 *
 * 1. The origin of this software must not be misrepresented; you must not
 * claim that you wrote the original software. If you use this software in a
 * product, an acknowledgment in the product documentation would be
 * appreciated but is not required.
 *
 * 2. Altered source versions must be plainly marked as such, and must not be
 * misrepresented as being the original software.
 *
 * 3. This notice may not be removed or altered from any source distribution.
 *
 * Ethan "flibitijibibo" Lee <flibitijibibo@flibitijibibo.com>
 *
 */
#endregion

#region Using Statements
using System;
using System.Runtime.InteropServices;
#endregion

/* We have lots of padding fields, which we want private */
#pragma warning disable 0169

namespace SDL2
{
	public static class SDL2
	{
		#region SDL2# Variables
		
		/* Used by DllImport to load the native library. */
		internal const String nativeLibName = "SDL2.dll";
		
		#endregion
		
		#region SDL_stdinc.h
		
		public static uint SDL_FOURCC(byte A, byte B, byte C, byte D)
		{
			return (uint) (A | (B << 8) | (C << 16) | (D << 24));
		}
		
		public enum SDL_bool
		{
			SDL_FALSE = 0,
			SDL_TRUE = 1
		}
		
		#endregion
		
		#region SDL.h
		
		public const uint SDL_INIT_TIMER =		0x00000001;
		public const uint SDL_INIT_AUDIO =		0x00000010;
		public const uint SDL_INIT_VIDEO =		0x00000020;
		public const uint SDL_INIT_JOYSTICK =		0x00000200;
		public const uint SDL_INIT_HAPTIC =		0x00001000;
		public const uint SDL_INIT_GAMECONTROLLER =	0x00002000;
		public const uint SDL_INIT_NOPARACHUTE =	0x00100000;
		public const uint SDL_INIT_EVERYTHING = (
			SDL_INIT_TIMER | SDL_INIT_AUDIO | SDL_INIT_VIDEO |
			SDL_INIT_JOYSTICK | SDL_INIT_HAPTIC |
			SDL_INIT_GAMECONTROLLER
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_Init(uint flags);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_InitSubSystem(uint flags);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_Quit();
		
		[DllImport(nativeLibName)]
		public static extern void SDL_QuitSubSystem(uint flags);
		
		[DllImport(nativeLibName)]
		public static extern uint SDL_WasInit(uint flags);
		
		#endregion
		
		#region SDL_hints.h
		
		public const string SDL_HINT_FRAMEBUFFER_ACCELERATION =
			"SDL_FRAMEBUFFER_ACCELERATION";
		public const string SDL_HINT_RENDER_DRIVER =
			"SDL_RENDER_DRIVER";
		public const string SDL_HINT_RENDER_OPENGL_SHADERS =
			"SDL_RENDER_OPENGL_SHADERS";
		public const string SDL_HINT_RENDER_VSYNC =
			"SDL_RENDER_VSYNC";
		public const string SDL_HINT_VIDEO_X11_XVIDMODE =
			"SDL_VIDEO_X11_XVIDMODE";
		public const string SDL_HINT_VIDEO_X11_XINERAMA =
			"SDL_VIDEO_X11_XINERAMA";
		public const string SDL_HINT_VIDEO_X11_XRANDR =
			"SDL_VIDEO_X11_XRANDR";
		public const string SDL_HINT_GRAB_KEYBOARD =
			"SDL_GRAB_KEYBOARD";
		public const string SDL_HINT_VIDEO_MINIMIZE_ON_FOCUS_LOSS =
			"SDL_VIDEO_MINIMIZE_ON_FOCUS_LOSS";
		public const string SDL_HINT_IDLE_TIMER_DISABLED =
			"SDL_IOS_DLE_TIMER_DISABLED";
		public const string SDL_HINT_ORIENTATIONS =
			"SDL_IOS_ORIENTATIONS";
		public const string SDL_HINT_XINPUT_ENABLED =
			"SDL_XINPUT_ENABLED";
		public const string SDL_HINT_GAMECONTROLLERCONFIG =
			"SDL_GAMECONTROLLERCONFIG";
		public const string SDL_HINT_ALLOW_TOPMOST =
			"SDL_ALLOW_TOPMOST";
		
		public enum SDL_HintPriority
		{
			SDL_HINT_DEFAULT,
			SDL_HINT_NORMAL,
			SDL_HINT_OVERRIDE
		}
		
		[DllImport(nativeLibName)]
		public static extern void SDL_ClearHints();
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetHint")]
		private static extern IntPtr INTERNAL_SDL_GetHint(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string name
		);
		public static string SDL_GetHint(string name)
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_SDL_GetHint(name)
			);
		}
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_SetHint(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string name,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string value
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_SetHintWithPriority(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string name,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string value,
			SDL_HintPriority priority
		);
		
		#endregion
		
		#region SDL_error.h
		
		[DllImport(nativeLibName)]
		public static extern void SDL_ClearError();
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetError")]
		private static extern IntPtr INTERNAL_SDL_GetError();
		public static string SDL_GetError()
		{
			return Marshal.PtrToStringAnsi(INTERNAL_SDL_GetError());
		}
		
		[DllImport(nativeLibName)]
		public static extern void SDL_SetError(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		#endregion
		
		#region SDL_log.h
		
		/* Begin nameless enum SDL_LOG_CATEGORY */
		public const int SDL_LOG_CATEGORY_APPLICATION = 0;
		public const int SDL_LOG_CATEGORY_ERROR = 1;
		public const int SDL_LOG_CATEGORY_ASSERT = 2;
		public const int SDL_LOG_CATEGORY_SYSTEM = 3;
		public const int SDL_LOG_CATEGORY_AUDIO = 4;
		public const int SDL_LOG_CATEGORY_VIDEO = 5;
		public const int SDL_LOG_CATEGORY_RENDER = 6;
		public const int SDL_LOG_CATEGORY_INPUT = 7;
		public const int SDL_LOG_CATEGORY_TEST = 8;
		
		/* Reserved for future SDL library use */
		public const int SDL_LOG_CATEGORY_RESERVED1 = 9;
		public const int SDL_LOG_CATEGORY_RESERVED2 = 10;
		public const int SDL_LOG_CATEGORY_RESERVED3 = 11;
		public const int SDL_LOG_CATEGORY_RESERVED4 = 12;
		public const int SDL_LOG_CATEGORY_RESERVED5 = 13;
		public const int SDL_LOG_CATEGORY_RESERVED6 = 14;
		public const int SDL_LOG_CATEGORY_RESERVED7 = 15;
		public const int SDL_LOG_CATEGORY_RESERVED8 = 16;
		public const int SDL_LOG_CATEGORY_RESERVED9 = 17;
		public const int SDL_LOG_CATEGORY_RESERVED10 = 18;
		
		/* Beyond this point is reserved for application use, e.g.
			enum {
				LOG_CATEGORY_AWESOME1 = SDL_LOG_CATEGORY_CUSTOM,
				LOG_CATEGORY_AWESOME2,
				LOG_CATEGORY_AWESOME3,
				...
			};
		*/
		public const int SDL_LOG_CATEGORY_CUSTOM = 19;
		/* End nameless enum SDL_LOG_CATEGORY */
		
		public enum SDL_LogPriority
		{
			SDL_LOG_PRIORITY_VERBOSE = 1,
			SDL_LOG_PRIORITY_DEBUG,
			SDL_LOG_PRIORITY_INFO,
			SDL_LOG_PRIORITY_WARN,
			SDL_LOG_PRIORITY_ERROR,
			SDL_LOG_PRIORITY_CRITICAL,
			SDL_NUM_LOG_PRIORITIES
		}
		
		[DllImport(nativeLibName)]
		public static extern void SDL_Log(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogVerbose(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogDebug(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogInfo(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogWarn(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogError(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogCritical(
			int category,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogMessage(
			int category,
			SDL_LogPriority priority,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogMessageV(
			int category,
			SDL_LogPriority priority,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string fmt,
			__arglist
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_LogPriority SDL_LogGetPriority(
			int category
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogSetPriority(
			int category,
			SDL_LogPriority priority
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogSetAllPriority(
			SDL_LogPriority priority
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_LogResetPriorities();
		
		// TODO: SDL_LogGetOutputFunction
		// TODO: SDL_LogSetOutputFunction
		
		#endregion
		
		#region SDL_version.h, SDL_revision.h
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_version
		{
			public byte major;
			public byte minor;
			public byte patch;
		}
		
		[DllImport(nativeLibName)]
		private static extern void SDL_GetVersion(ref SDL_version ver);
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetRevision")]
		private static extern IntPtr INTERNAL_SDL_GetRevision();
		public static string SDL_GetRevision()
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_SDL_GetRevision()
			);
		}
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetRevisionNumber();
		
		#endregion
		
		#region SDL_video.h
		
		/* Actually, this is from SDL_blendmode.h */
		public enum SDL_BlendMode
		{
			SDL_BLENDMODE_NONE =	0x00000000,
			SDL_BLENDMODE_BLEND =	0x00000001,
			SDL_BLENDMODE_ADD =	0x00000002,
			SDL_BLENDMODE_MOD =	0x00000004
		}
		
		public enum SDL_GLattr
		{
			SDL_GL_RED_SIZE,
			SDL_GL_GREEN_SIZE,
			SDL_GL_BLUE_SIZE,
			SDL_GL_ALPHA_SIZE,
			SDL_GL_BUFFER_SIZE,
			SDL_GL_DOUBLEBUFFER,
			SDL_GL_DEPTH_SIZE,
			SDL_GL_STENCIL_SIZE,
			SDL_GL_ACCUM_RED_SIZE,
			SDL_GL_ACCUM_GREEN_SIZE,
			SDL_GL_ACCUM_BLUE_SIZE,
			SDL_GL_ACCUM_ALPHA_SIZE,
			SDL_GL_STEREO,
			SDL_GL_MULTISAMPLEBUFFERS,
			SDL_GL_MULTISAMPLESAMPLES,
			SDL_GL_ACCELERATED_VISUAL,
			SDL_GL_RETAINED_BACKING,
			SDL_GL_CONTEXT_MAJOR_VERSION,
			SDL_GL_CONTEXT_MINOR_VERSION,
			SDL_GL_CONTEXT_EGL,
			SDL_GL_CONTEXT_FLAGS,
			SDL_GL_PROFILE_MASK,
			SDL_GL_SHARE_WITH_CURRENT_CONTEXT
		}
		
		public enum SDL_WindowEventID : byte
		{
			SDL_WINDOWEVENT_NONE,
			SDL_WINDOWEVENT_SHOWN,
			SDL_WINDOWEVENT_HIDDEN,
			SDL_WINDOWEVENT_EXPOSED,
			SDL_WINDOWEVENT_MOVED,
			SDL_WINDOWEVENT_RESIZED,
			SDL_WINDOWEVENT_SIZE_CHANGED,
			SDL_WINDOWEVENT_MINIMIZED,
			SDL_WINDOWEVENT_MAXIMIZED,
			SDL_WINDOWEVENT_RESTORED,
			SDL_WINDOWEVENT_ENTER,
			SDL_WINDOWEVENT_LEAVE,
			SDL_WINDOWEVENT_FOCUS_GAINED,
			SDL_WINDOWEVENT_FOCUS_LOST,
			SDL_WINDOWEVENT_CLOSE,
		}
		
		public enum SDL_WindowFlags
		{
			SDL_WINDOW_FULLSCREEN =		0x00000001,
			SDL_WINDOW_OPENGL =		0x00000002,
			SDL_WINDOW_SHOWN =		0x00000004,
			SDL_WINDOW_HIDDEN =		0x00000008,
			SDL_WINDOW_BORDERLESS =		0x00000010,
			SDL_WINDOW_RESIZABLE =		0x00000020,
			SDL_WINDOW_MINIMIZED =		0x00000040,
			SDL_WINDOW_MAXIMIZED =		0x00000080,
			SDL_WINDOW_INPUT_GRABBED =	0x00000100,
			SDL_WINDOW_INPUT_FOCUS =	0x00000200,
			SDL_WINDOW_MOUSE_FOCUS =	0x00000400,
			SDL_WINDOW_FULLSCREEN_DESKTOP =
				(SDL_WINDOW_FULLSCREEN | 0x00001000),
			SDL_WINDOW_FOREIGN =		0x00000800,
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_DisplayMode
		{
			public uint format;
			public int w;
			public int h;
			public int refresh_rate;
			public IntPtr driverdata; // void*
		}
		
		/* IntPtr refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_CreateWindow(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string title,
			int x,
			int y,
			int w,
			int h,
			uint flags
		);
		
		/* window and renderer refer to an SDL_Window* and SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_CreateWindowAndRenderer(
			int width,
			int height,
			uint window_flags,
			ref IntPtr window,
			ref IntPtr renderer
		);
		
		/* IntPtr refers to an SDL_Window*. data is a void* pointer. */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_CreateWindowFrom(IntPtr data);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_DestroyWindow(IntPtr window);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_DisableScreenSaver();
		
		[DllImport(nativeLibName)]
		public static extern void SDL_EnableScreenSaver();
		
		/* IntPtr refers to an SDL_DisplayMode. Just use closest. */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_GetClosestDisplayMode(
			int displayIndex,
			ref SDL_DisplayMode mode,
			ref SDL_DisplayMode closest
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetCurrentDisplayMode(
			int displayIndex,
			ref SDL_DisplayMode mode
		);
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetCurrentVideoDriver")]
		private static extern IntPtr INTERNAL_SDL_GetCurrentVideoDriver();
		public static string SDL_GetCurrentVideoDriver()
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_SDL_GetCurrentVideoDriver()
			);
		}
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetDesktopDisplayMode(
			int displayIndex,
			ref SDL_DisplayMode mode
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetDisplayBounds(
			int displayIndex,
			ref SDL_Rect rect
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetDisplayMode(
			int displayIndex,
			int modeIndex,
			ref SDL_DisplayMode mode
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetNumDisplayModes(
			int displayIndex
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetNumVideoDisplays();
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetNumVideoDrivers();
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetVideoDriver")]
		private static extern IntPtr INTERNAL_SDL_GetVideoDriver(
			int index
		);
		public static string SDL_GetVideoDriver(int index)
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_SDL_GetVideoDriver(index)
			);
		}
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern float SDL_GetWindowBrightness(
			IntPtr window
		);
		
		/* IntPtr refers to void* data. window refers to an SDL_Window*. */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_GetWindowData(
			IntPtr window,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string name
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetWindowDisplayIndex(
			IntPtr window
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetWindowDisplayMode(
			IntPtr window,
			ref SDL_DisplayMode mode
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern uint SDL_GetWindowFlags(IntPtr window);
		
		/* IntPtr refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_GetWindowFromID(uint id);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetWindowGammaRamp(
			IntPtr window,
			ref ushort red,
			ref ushort green,
			ref ushort blue
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_GetWindowGrab(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern uint SDL_GetWindowID(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern uint SDL_GetWindowPixelFormat(
			IntPtr window
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_GetWindowPosition(
			IntPtr window,
			ref int x,
			ref int y
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_GetWindowSize(
			IntPtr window,
			ref int w,
			ref int h
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, EntryPoint = "SDL_GetWindowSurface")]
		private static extern IntPtr INTERNAL_SDL_GetWindowSurface(
			IntPtr window
		);
		public static SDL_Surface SDL_GetWindowSurface(IntPtr window)
		{
			SDL_Surface result;
			IntPtr result_ptr = INTERNAL_SDL_GetWindowSurface(
				window
			);
			result = (SDL_Surface) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName, EntryPoint = "SDL_GetWindowTitle")]
		private static extern IntPtr INTERNAL_SDL_GetWindowTitle(
			IntPtr window
		);
		public static string SDL_GetWindowTitle(IntPtr window)
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_SDL_GetWindowTitle(window)
			);
		}
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GL_BindTexture(
			IntPtr texture,
			ref float texw,
			ref float texh
		);
		
		/* IntPtr and window refer to an SDL_GLContext and SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_GL_CreateContext(IntPtr window);
		
		/* context refers to an SDL_GLContext */
		[DllImport(nativeLibName)]
		public static extern void SDL_GL_DeleteContext(IntPtr context);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_GL_ExtensionSupported(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string extension
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GL_GetAttribute(
			SDL_GLattr attr,
			ref int value
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GL_GetSwapInterval();
		
		/* window and context refer to an SDL_Window* and SDL_GLContext */
		[DllImport(nativeLibName)]
		public static extern int SDL_GL_MakeCurrent(
			IntPtr window,
			IntPtr context
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GL_SetAttribute(
			SDL_GLattr attr,
			int value
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GL_SetSwapInterval(int interval);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_GL_SwapWindow(IntPtr window);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GL_UnbindTexture(IntPtr texture);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_HideWindow(IntPtr window);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_IsScreenSaverEnabled();
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_MaximizeWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_MinimizeWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_RaiseWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_RestoreWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetWindowBrightness(
			IntPtr window,
			float brightness
		);
		
		/* IntPtr and userdata are void*, window is an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_SetWindowData(
			IntPtr window,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string name,
			IntPtr userdata
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetWindowDisplayMode(
			IntPtr window,
			ref SDL_DisplayMode mode
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetWindowFullscreen(
			IntPtr window,
			SDL_bool fullscreen
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetWindowGammaRamp(
			IntPtr window,
			ref ushort red,
			ref ushort green,
			ref ushort blue
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_SetWindowGrab(
			IntPtr window,
			SDL_bool grabbed
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_SetWindowIcon(
			IntPtr window,
			ref SDL_Surface icon
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_SetWindowPosition(
			IntPtr window,
			int x,
			int y
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_SetWindowSize(
			IntPtr window,
			int w,
			int h
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_SetWindowTitle(
			IntPtr window,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string title
		);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern void SDL_ShowWindow(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_UpdateWindowSurface(IntPtr window);
		
		/* window refers to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern int SDL_UpdateWindowSurfaceRects(
			IntPtr window,
			SDL_Rect[] rects,
			int numrects
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_VideoInit(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string driver_name
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_VideoQuit();
		
		#endregion
		
		#region SDL_render.h
		
		public enum SDL_RendererFlags
		{
			SDL_RENDERER_SOFTWARE =		0x00000001,
			SDL_RENDERER_ACCELERATED =	0x00000002,
			SDL_RENDERER_PRESENTVSYNC =	0x00000004,
			SDL_RENDERER_TARGETTEXTURE =	0x00000008
		}
		
		public enum SDL_RendererFlip
		{
			SDL_FLIP_NONE =		0x00000000,
			SDL_FLIP_HORIZONTAL =	0x00000001,
			SDL_FLIP_VERTICAL =	0x00000002
		}
		
		public enum SDL_TextureAccess
		{
			SDL_TEXTUREACCES_STATIC,
			SDL_TEXTUREACCESS_STREAMING,
			SDL_TEXTUREACCESS_TARGET
		}
		
		public enum SDL_TextureModulate
		{
			SDL_TEXTUREMODULATE_NONE =		0x00000000,
			SDL_TEXTUREMODULATE_HORIZONTAL =	0x00000001,
			SDL_TEXTUREMODULATE_VERTICAL =		0x00000002
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public unsafe struct SDL_RendererInfo
		{
			public string name; // const char*
			public uint flags;
			public uint num_texture_formats;
			public fixed uint texture_formats[16];
			public int max_texture_width;
			public int max_texture_height;
		}
		
		/* IntPtr refers to an SDL_Renderer*, window to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_CreateRenderer(
			IntPtr window,
			int index,
			uint flags
		);
		
		/* IntPtr refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_CreateRenderer(
			ref SDL_Surface surface
		);
		
		/* IntPtr refers to an SDL_Texture*, renderer to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_CreateTexture(
			IntPtr renderer,
			uint format,
			int access,
			int w,
			int h
		);
		
		/* IntPtr refers to an SDL_Texture*, renderer to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_CreateTextureFromSurface(
			IntPtr renderer,
			ref SDL_Surface surface
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern void SDL_DestroyRenderer(IntPtr renderer);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern void SDL_DestroyTexture(IntPtr texture);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetNumRenderDrivers();
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetRenderDrawBlendMode(
			IntPtr renderer,
			ref SDL_BlendMode blendMode
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetRenderDrawColor(
			IntPtr renderer,
			ref byte r,
			ref byte g,
			ref byte b,
			ref byte a
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetRenderDriverInfo(
			int index,
			ref SDL_RendererInfo info
		);
		
		/* IntPtr refers to an SDL_Renderer*, window to an SDL_Window* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_GetRenderer(IntPtr window);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetRendererInfo(
			IntPtr renderer,
			ref SDL_RendererInfo info
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetTextureAlphaMod(
			IntPtr texture,
			ref byte alpha
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetTextureBlendMode(
			IntPtr texture,
			ref SDL_BlendMode blendMode
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_GetTextureColorMod(
			IntPtr texture,
			ref byte r,
			ref byte g,
			ref byte b
		);
		
		/* texture refers to an SDL_Texture*, pixels to a void* */
		[DllImport(nativeLibName)]
		public static extern int SDL_LockTexture(
			IntPtr texture,
			ref SDL_Rect rect,
			ref IntPtr pixels,
			ref int pitch
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_QueryTexture(
			IntPtr texture,
			ref uint format,
			ref int access,
			ref int w,
			ref int h
		);
		
		/* texture refers to an SDL_Texture, pixels to a void* */
		[DllImport(nativeLibName)]
		public static extern int SDL_QueryTexturePixels(
			IntPtr texture,
			ref IntPtr pixels,
			ref int pitch
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderClear(IntPtr renderer);
		
		/* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderCopy(
			IntPtr renderer,
			IntPtr texture,
			ref SDL_Rect srcrect,
			ref SDL_Rect dstrect
		);
		
		/* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderCopyEx(
			IntPtr renderer,
			IntPtr texture,
			ref SDL_Rect srcrect,
			ref SDL_Rect dstrect,
			double angle,
			ref SDL_Point center,
			SDL_RendererFlip flip
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderDrawLine(
			IntPtr renderer,
			int x1,
			int y1,
			int x2,
			int y2
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderDrawLines(
			IntPtr renderer,
			SDL_Point[] points,
			int count
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderDrawPoint(
			IntPtr renderer,
			int x,
			int y
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderDrawPoints(
			IntPtr renderer,
			SDL_Point[] points,
			int count
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderDrawRect(
			IntPtr renderer,
			ref SDL_Rect rect
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderDrawRects(
			IntPtr renderer,
			SDL_Rect[] rects,
			int count
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderFillRect(
			IntPtr renderer,
			ref SDL_Rect rect
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderFillRects(
			IntPtr renderer,
			SDL_Rect[] rects,
			int count
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RendererGetViewport(
			IntPtr renderer,
			ref SDL_Rect rect
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern void SDL_RenderPresent(IntPtr renderer);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderReadPixels(
			IntPtr renderer,
			ref SDL_Rect rect,
			uint format,
			IntPtr pixels,
			int pitch
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_RenderSetViewport(
			IntPtr renderer,
			ref SDL_Rect rect
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetRenderDrawBlendMode(
			IntPtr renderer,
			SDL_BlendMode blendMode
		);
		
		/* renderer refers to an SDL_Renderer* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetRenderDrawColor(
			IntPtr renderer,
			byte r,
			byte g,
			byte b,
			byte a
		);
		
		/* renderer refers to an SDL_Renderer*, texture to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetRenderTarget(
			IntPtr renderer,
			IntPtr texture
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetTextureAlphaMod(
			IntPtr texture,
			byte alpha
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetTextureBlendMode(
			IntPtr texture,
			SDL_BlendMode blendMode
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetTextureColorMod(
			IntPtr texture,
			byte r,
			byte g,
			byte b
		);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern void SDL_UnlockTexture(IntPtr texture);
		
		/* texture refers to an SDL_Texture* */
		[DllImport(nativeLibName)]
		public static extern int SDL_UpdateTexture(
			IntPtr texture,
			ref SDL_Rect rect,
			IntPtr pixels,
			int pitch
		);
		
		#endregion
		
		#region SDL_pixels.h
		
		public static uint SDL_DEFINE_PIXELFOURCC(byte A, byte B, byte C, byte D)
		{
			return SDL_FOURCC(A, B, C, D);
		}
		
		public static uint SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM type,
			SDL_PIXELORDER_ENUM order,
			SDL_PACKEDLAYOUT_ENUM layout,
			byte bits,
			byte bytes
		) {
			return (uint) (
				(1 << 28) |
				(((byte) type) << 24) |
				(((byte) order) << 20) |
				(((byte) layout) << 16) |
				(bits << 8) |
				(bytes)
			);
		}
		
		public static byte SDL_PIXELFLAG(uint X)
		{
			return (byte) ((X >> 28) & 0x0F);
		}
		
		public static byte SDL_PIXELTYPE(uint X)
		{
			return (byte) ((X >> 24) & 0x0F);
		}
		
		public static byte SDL_PIXELORDER(uint X)
		{
			return (byte) ((X >> 20) & 0x0F);
		}
		
		public static byte SDL_BITSPERPIXEL(uint X)
		{
			return (byte) ((X << 16) & 0x0F);
		}
		
		public static byte SDL_BYTESPERPIXEL(uint X)
		{
			if (SDL_ISPIXELFORMAT_FOURCC(X))
			{
				if (	(X == SDL_PIXELFORMAT_YUY2) ||
						(X == SDL_PIXELFORMAT_UYVY) ||
						(X == SDL_PIXELFORMAT_YVYU)	)
				{
					return 2;
				}
				return 1;
			}
			return (byte) (X & 0xFF);
		}
		
		public static bool SDL_ISPIXELFORMAT_INDEXED(uint format)
		{
			if (SDL_ISPIXELFORMAT_FOURCC(format))
			{
				return false;
			}
			SDL_PIXELTYPE_ENUM pType =
					(SDL_PIXELTYPE_ENUM) SDL_PIXELTYPE(format);
			return (
				pType == SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX1 ||
			    pType == SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX4 ||
			    pType == SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX8
			);
		}
		
		public static bool SDL_ISPIXELFORMAT_ALPHA(uint format)
		{
			if (SDL_ISPIXELFORMAT_FOURCC(format))
			{
				return false;
			}
			SDL_PIXELORDER_ENUM pOrder =
					(SDL_PIXELORDER_ENUM) SDL_PIXELORDER(format);
			return (
				pOrder == SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB ||
			    pOrder == SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBA ||
			    pOrder == SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ABGR ||
			    pOrder == SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRA
			);
		}
		
		public static bool SDL_ISPIXELFORMAT_FOURCC(uint format)
		{
			return (format == 0) && (SDL_PIXELFLAG(format) != 1);
		}
		
		public enum SDL_PIXELTYPE_ENUM
		{
			SDL_PIXELTYPE_UNKNOWN,
			SDL_PIXELTYPE_INDEX1,
			SDL_PIXELTYPE_INDEX4,
			SDL_PIXELTYPE_INDEX8,
			SDL_PIXELTYPE_PACKED8,
			SDL_PIXELTYPE_PACKED16,
			SDL_PIXELTYPE_PACKED32,
			SDL_PIXELTYPE_ARRAYU8,
			SDL_PIXELTYPE_ARRAYU16,
			SDL_PIXELTYPE_ARRAYU32,
			SDL_PIXELTYPE_ARRAYF16,
			SDL_PIXELTYPE_ARRAYF32
		}
		
		public enum SDL_PIXELORDER_ENUM
		{
			/* BITMAPORDER */
			SDL_BITMAPORDER_NONE,
			SDL_BITMAPORDER_4321,
			SDL_BITMAPORDER_1234,
			/* PACKEDORDER */
			SDL_PACKEDORDER_NONE = 0,
			SDL_PACKEDORDER_XRGB,
			SDL_PACKEDORDER_RGBX,
			SDL_PACKEDORDER_ARGB,
			SDL_PACKEDORDER_RGBA,
			SDL_PACKEDORDER_XBGR,
			SDL_PACKEDORDER_BGRX,
			SDL_PACKEDORDER_ABGR,
			SDL_PACKEDORDER_BGRA,
			/* ARRAYORDER */
			SDL_ARRAYORDER_NONE = 0,
			SDL_ARRAYORDER_RGB,
			SDL_ARRAYORDER_RGBA,
			SDL_ARRAYORDER_ARGB,
			SDL_ARRAYORDER_BGR,
			SDL_ARRAYORDER_BGRA,
			SDL_ARRAYORDER_ABGR
		}
		
		public enum SDL_PACKEDLAYOUT_ENUM
		{
			SDL_PACKEDLAYOUT_NONE,
			SDL_PACKEDLAYOUT_332,
			SDL_PACKEDLAYOUT_4444,
			SDL_PACKEDLAYOUT_1555,
			SDL_PACKEDLAYOUT_5551,
			SDL_PACKEDLAYOUT_565,
			SDL_PACKEDLAYOUT_8888,
			SDL_PACKEDLAYOUT_2101010,
			SDL_PACKEDLAYOUT_1010102
		}
		
		/* FIXME: These should all be const!!! */
		public const uint SDL_PIXELFORMAT_UNKNOWN = 0;
		public static uint SDL_PIXELFORMAT_INDEX1LSB = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX1,
			SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_4321,
			0,
			1, 0
		);
		public static uint SDL_PIXELFORMAT_INDEX1MSB = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX1,
			SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_1234,
			0,
			1, 0
		);
		public static uint SDL_PIXELFORMAT_INDEX4LSB = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX4,
			SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_4321,
			0,
			4, 0
		);
		public static uint SDL_PIXELFORMAT_INDEX4MSB = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX4,
			SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_1234,
			0,
			4, 0
		);
		public static uint SDL_PIXELFORMAT_INDEX8 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX8,
			0,
			0,
			8, 1
		);
		public static uint SDL_PIXELFORMAT_RGB332 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED8,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_332,
			8, 1
		);
		public static uint SDL_PIXELFORMAT_RGB444 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
			12, 2
		);
		public static uint SDL_PIXELFORMAT_RGB555 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_1555,
			15, 2
		);
		public static uint SDL_PIXELFORMAT_BGR555 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_INDEX1,
			SDL_PIXELORDER_ENUM.SDL_BITMAPORDER_4321,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_1555,
			15, 2
		);
		public static uint SDL_PIXELFORMAT_ARGB4444 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_RGBA4444 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBA,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_ABGR4444 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ABGR,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_BGRA4444 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRA,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_4444,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_ARGB1555 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_1555,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_RGBA5551 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBA,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_5551,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_ABGR1555 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ABGR,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_1555,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_BGRA5551 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRA,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_5551,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_RGB565 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_565,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_BGR565 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED16,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XBGR,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_565,
			16, 2
		);
		public static uint SDL_PIXELFORMAT_RGB24 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_ARRAYU8,
			SDL_PIXELORDER_ENUM.SDL_ARRAYORDER_RGB,
			0,
			24, 3
		);
		public static uint SDL_PIXELFORMAT_BGR24 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_ARRAYU8,
			SDL_PIXELORDER_ENUM.SDL_ARRAYORDER_BGR,
			0,
			24, 3
		);
		public static uint SDL_PIXELFORMAT_RGB888 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XRGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
			24, 4
		);
		public static uint SDL_PIXELFORMAT_RGBX8888 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBX,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
			24, 4
		);
		public static uint SDL_PIXELFORMAT_BGR888 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_XBGR,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
			24, 4
		);
		public static uint SDL_PIXELFORMAT_BGRX8888 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRX,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
			24, 4
		);
		public static uint SDL_PIXELFORMAT_ARGB8888 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
			32, 4
		);
		public static uint SDL_PIXELFORMAT_RGBA8888 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_RGBA,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
			32, 4
		);
		public static uint SDL_PIXELFORMAT_ABGR8888 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ABGR,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
			32, 4
		);
		public static uint SDL_PIXELFORMAT_BGRA8888 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_BGRA,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_8888,
			32, 4
		);
		public static uint SDL_PIXELFORMAT_ARGB2101010 = SDL_DEFINE_PIXELFORMAT(
			SDL_PIXELTYPE_ENUM.SDL_PIXELTYPE_PACKED32,
			SDL_PIXELORDER_ENUM.SDL_PACKEDORDER_ARGB,
			SDL_PACKEDLAYOUT_ENUM.SDL_PACKEDLAYOUT_2101010,
			32, 4
		);
		public static uint SDL_PIXELFORMAT_YV12 = SDL_DEFINE_PIXELFOURCC(
			(byte) 'Y', (byte) 'V', (byte) '1', (byte) '2'
		);
		public static uint SDL_PIXELFORMAT_IYUV = SDL_DEFINE_PIXELFOURCC(
			(byte) 'I', (byte) 'Y', (byte) 'U', (byte) 'V'
		);
		public static uint SDL_PIXELFORMAT_YUY2 = SDL_DEFINE_PIXELFOURCC(
			(byte) 'Y', (byte) 'U', (byte) 'Y', (byte) '2'
		);
		public static uint SDL_PIXELFORMAT_UYVY = SDL_DEFINE_PIXELFOURCC(
			(byte) 'U', (byte) 'Y', (byte) 'V', (byte) 'Y'
		);
		public static uint SDL_PIXELFORMAT_YVYU = SDL_DEFINE_PIXELFOURCC(
			(byte) 'Y', (byte) 'V', (byte) 'Y', (byte) 'U'
		);
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Color
		{
			public byte r;
			public byte g;
			public byte b;
			public byte a;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Palette
		{
			public int ncolors;
			public SDL_Color[] colors;
			public int version;
			public int refcount;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_PixelFormat
		{
			public uint format;
			public IntPtr palette; // SDL_Palette*
			public byte BitsPerPixel;
			public byte BytesPerPixel;
			public uint Rmask;
			public uint Gmask;
			public uint Bmask;
			public uint Amask;
			public byte Rloss;
			public byte Gloss;
			public byte Bloss;
			public byte Aloss;
			public byte Rshift;
			public byte Gshift;
			public byte Bshift;
			public byte Ashift;
			public int refcount;
			public IntPtr next; // SDL_PixelFormat*
		}
		
		/* IntPtr refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_AllocFormat(uint pixel_format);
		
		/* IntPtr refers to an SDL_Palette* */
		[DllImport(nativeLibName)]
		public static extern IntPtr SDL_AllocPalette(int ncolors);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_CalculateGammaRamp(
			float gamma,
			ref ushort ramp
		);
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName)]
		public static extern void SDL_FreeFormat(IntPtr format);
		
		/* palette refers to an SDL_Palette* */
		[DllImport(nativeLibName)]
		public static extern void SDL_FreePalette(IntPtr palette);
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetPixelFormatName")]
		private static extern IntPtr INTERNAL_SDL_GetPixelFormatName(
			uint format
		);
		public static string SDL_GetPixelFormatName(uint format)
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_SDL_GetPixelFormatName(format)
			);
		}
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName)]
		public static extern void SDL_GetRGB(
			uint pixel,
			IntPtr format,
			ref byte r,
			ref byte g,
			ref byte b
		);
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName)]
		public static extern void SDL_GetRGBA(
			uint pixel,
			IntPtr format,
			ref byte r,
			ref byte g,
			ref byte b,
			ref byte a
		);
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName)]
		public static extern uint SDL_MapRGB(
			IntPtr format,
			byte r,
			byte g,
			byte b
		);
		
		/* format refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName)]
		public static extern uint SDL_MapRGBA(
			IntPtr format,
			byte r,
			byte g,
			byte b,
			byte a
		);
		
		[DllImport(nativeLibName)]
		public static extern uint SDL_MasksToPixelFormatEnum(
			int bpp,
			uint Rmask,
			uint Gmask,
			uint Bmask,
			uint Amask
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_PixelFormatEnumToMasks(
			uint format,
			ref int bpp,
			ref uint Rmask,
			ref uint Gmask,
			ref uint Bmask,
			ref uint Amask
		);
		
		/* palette refers to an SDL_Palette* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetPaletteColors(
			IntPtr palette,
			SDL_Color[] colors,
			int firstcolor,
			int ncolors
		);
		
		/* format and palette refer to an SDL_PixelFormat* and SDL_Palette* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetPixelFormatPalette(
			IntPtr format,
			IntPtr palette
		);
		
		#endregion
		
		#region SDL_rect.h
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Point
		{
			public int x;
			public int y;
		}
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Rect
		{
			public int x;
			public int y;
			public int w;
			public int h;
		}
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_EnclosePoints(
			SDL_Point[] points,
			int count,
			ref SDL_Rect clip,
			ref SDL_Rect result
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_HasIntersection(
			ref SDL_Rect A,
			ref SDL_Rect B
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_IntersectRect(
			ref SDL_Rect A,
			ref SDL_Rect B,
			ref SDL_Rect result
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_IntersectRectAndLine(
			ref SDL_Rect rect,
			ref int X1,
			ref int Y1,
			ref int X2,
			ref int Y2
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_RectEmpty(ref SDL_Rect rect);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_RectEquals(
			ref SDL_Rect A,
			ref SDL_Rect B
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_UnionRect(
			ref SDL_Rect A,
			ref SDL_Rect B,
			ref SDL_Rect result
		);
		
		#endregion
		
		#region SDL_surface.h
		
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_Surface
		{
			public uint flags;
			public IntPtr format; // SDL_PixelFormat*
			public int w;
			public int h;
			public int pitch;
			public IntPtr pixels; // void*
			public IntPtr userdata; // void*
			public int locked;
			public IntPtr lock_data; // void*
			public SDL_Rect clip_rect;
			public IntPtr map; // SDL_BlitMap*
			public int refcount;
		}
		
		[DllImport(nativeLibName)]
		public static extern int SDL_BlitSurface(
			ref SDL_Surface src,
			ref SDL_Rect srcrect,
			ref SDL_Surface dst,
			ref SDL_Rect dstrect
		);
		
		/* src and dst are void* pointers */
		[DllImport(nativeLibName)]
		public static extern int SDL_ConvertPixels(
			int width,
			int height,
			uint src_format,
			IntPtr src,
			int src_pitch,
			uint dst_format,
			IntPtr dst,
			int dst_pitch
		);
		
		/* fmt refers to an SDL_PixelFormat* */
		[DllImport(nativeLibName, EntryPoint = "SDL_ConvertSurface")]
		private static extern IntPtr INTERNAL_SDL_ConvertSurface(
			ref SDL_Surface src,
			IntPtr fmt,
			uint flags
		);
		public static SDL_Surface SDL_ConvertSurface(
			ref SDL_Surface src,
			IntPtr fmt,
			uint flags
		) {
			SDL_Surface result;
			IntPtr result_ptr = INTERNAL_SDL_ConvertSurface(
				ref src,
				fmt,
				flags
			);
			result = (SDL_Surface) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		[DllImport(nativeLibName, EntryPoint = "SDL_ConvertSurfaceFormat")]
		private static extern IntPtr INTERNAL_SDL_ConvertSurfaceFormat(
			ref SDL_Surface src,
			uint pixel_format,
			uint flags
		);
		public static SDL_Surface SDL_ConvertSurfaceFormat(
			ref SDL_Surface src,
			uint pixel_format,
			uint flags
		) {
			SDL_Surface result;
			IntPtr result_ptr = INTERNAL_SDL_ConvertSurfaceFormat(
				ref src,
				pixel_format,
				flags
			);
			result = (SDL_Surface) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		[DllImport(nativeLibName, EntryPoint = "SDL_CreateRGBSurface")]
		private static extern IntPtr INTERNAL_SDL_CreateRGBSurface(
			uint flags,
			int width,
			int height,
			int depth,
			uint Rmask,
			uint Gmask,
			uint Bmask,
			uint Amask
		);
		public static SDL_Surface SDL_CreateRGBSurface(
			uint flags,
			int width,
			int height,
			int depth,
			uint Rmask,
			uint Gmask,
			uint Bmask,
			uint Amask
		) {
			SDL_Surface result;
			IntPtr result_ptr = INTERNAL_SDL_CreateRGBSurface(
				flags,
				width,
				height,
				depth,
				Rmask,
				Gmask,
				Bmask,
				Amask
			);
			result = (SDL_Surface) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		/* pixels refers to a void* */
		[DllImport(nativeLibName, EntryPoint = "SDL_CreateRGBSurfaceFrom")]
		private static extern IntPtr INTERNAL_SDL_CreateRGBSurfaceFrom(
			IntPtr pixels,
			int width,
			int height,
			int depth,
			int pitch,
			uint Rmask,
			uint Gmask,
			uint Bmask,
			uint Amask
		);
		public static SDL_Surface SDL_CreateRGBSurfaceFrom(
			IntPtr pixels,
			int width,
			int height,
			int depth,
			int pitch,
			uint Rmask,
			uint Gmask,
			uint Bmask,
			uint Amask
		) {
			SDL_Surface result;
			IntPtr result_ptr = INTERNAL_SDL_CreateRGBSurfaceFrom(
				pixels,
				width,
				height,
				depth,
				pitch,
				Rmask,
				Gmask,
				Bmask,
				Amask
			);
			result = (SDL_Surface) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		[DllImport(nativeLibName)]
		public static extern int SDL_FillRect(
			ref SDL_Surface dst,
			ref SDL_Rect rect,
			uint color
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_FillRects(
			ref SDL_Surface dst,
			SDL_Rect[] rects,
			int count,
			uint color
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_FreeSurface(ref SDL_Surface surface);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_GetClipRect(
			ref SDL_Surface surface,
			ref SDL_Rect rect
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetColorKey(
			ref SDL_Surface surface,
			ref uint key
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetSurfaceAlphaMod(
			ref SDL_Surface surface,
			ref byte alpha
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetSurfaceBlendMode(
			ref SDL_Surface surface,
			ref SDL_BlendMode blendMode
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_GetSurfaceColorMod(
			ref SDL_Surface surface,
			ref byte r,
			ref byte g,
			ref byte b
		);
		
		[DllImport(nativeLibName, EntryPoint = "SDL_LoadBMP")]
		private static extern IntPtr INTERNAL_SDL_LoadBMP(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string file
		);
		public static SDL_Surface SDL_LoadBMP(string file)
		{
			SDL_Surface result;
			IntPtr result_ptr = INTERNAL_SDL_LoadBMP(file);
			result = (SDL_Surface) Marshal.PtrToStructure(
				result_ptr,
				result.GetType()
			);
			return result;
		}
		
		[DllImport(nativeLibName)]
		public static extern int SDL_LockSurface(ref SDL_Surface surface);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_LowerBlit(
			ref SDL_Surface src,
			ref SDL_Rect srcrect,
			ref SDL_Surface dst,
			ref SDL_Rect dstrect
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_LowerBlitScaled(
			ref SDL_Surface src,
			ref SDL_Rect srcrect,
			ref SDL_Surface dst,
			ref SDL_Rect dstrect
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_MUSTLOCK(ref SDL_Surface surface);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SaveBMP(
			ref SDL_Surface surface,
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string file
		);
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_SetClipRect(
			ref SDL_Surface surface,
			ref SDL_Rect rect
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SetColorKey(
			ref SDL_Surface surface,
			int flag,
			uint key
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SetSurfaceAlphaMod(
			ref SDL_Surface surface,
			byte alpha
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SetSurfaceBlendMode(
			ref SDL_Surface surface,
			SDL_BlendMode blendMode
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SetSurfaceColorMod(
			ref SDL_Surface surface,
			byte r,
			byte g,
			byte b
		);
		
		/* palette refers to an SDL_Palette* */
		[DllImport(nativeLibName)]
		public static extern int SDL_SetSurfacePalette(
			ref SDL_Surface surface,
			IntPtr palette
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SetSurfaceRLE(
			ref SDL_Surface surface,
			int flag
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SoftStretch(
			ref SDL_Surface src,
			ref SDL_Rect srcrect,
			ref SDL_Surface dst,
			ref SDL_Rect dstrect
		);
		
		[DllImport(nativeLibName)]
		public static extern void SDL_UnlockSurface(ref SDL_Surface surface);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_UpperBlit(
			ref SDL_Surface src,
			ref SDL_Rect srcrect,
			ref SDL_Surface dst,
			ref SDL_Rect dstrect
		);
		
		[DllImport(nativeLibName)]
		public static extern int SDL_UpperBlitScaled(
			ref SDL_Surface src,
			ref SDL_Rect srcrect,
			ref SDL_Surface dst,
			ref SDL_Rect dstrect
		);
		
		#endregion
		
		#region SDL_clipboard.h
		
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_HasClipboardText();
		
		[DllImport(nativeLibName, EntryPoint = "SDL_GetClipboardText")]
		private static extern IntPtr INTERNAL_SDL_GetClipboardText();
		public static string SDL_GetClipboardText(string name)
		{
			return Marshal.PtrToStringAnsi(
				INTERNAL_SDL_GetClipboardText()
			);
		}
		
		[DllImport(nativeLibName)]
		public static extern int SDL_SetClipboardText(
			[InAttribute()] [MarshalAsAttribute(UnmanagedType.LPStr)]
				string text
		);
		
		#endregion
		
		/* TODO: Input Events:
		 * http://wiki.libsdl.org/moin.fcg/APIByCategory#Input_Events
		 */
		#region SDL_events.h

		/* General keyboard/mouse state definitions. */
		public const byte SDL_PRESSED =		0;
		public const byte SDL_RELEASED =	1;

		/* The types of events that can be delivered. */
		public enum SDL_EventType : uint
		{
			SDL_FIRSTEVENT =		0,

			/* Application events */
			SDL_QUIT = 			0x100,

			/* Window events */
			SDL_WINDOWEVENT = 		0x200,
			SDL_SYSWMEVENT,
			
			/* Keyboard events */
			SDL_KEYDOWN = 			0x300,
			SDL_KEYUP,
			SDL_TEXTEDITING,
			SDL_TEXTINPUT,

			/* Mouse events */
			SDL_MOUSEMOTION = 		0x400,
			SDL_MOUSEBUTTONDOWN,
			SDL_MOUSEBUTTONUP,
			SDL_MOUSEWHEEL,

			/* Joystick events */
			SDL_JOYAXISMOTION =		0x600,
			SDL_JOYBALLMOTION,
			SDL_JOYHATMOTION,
			SDL_JOYBUTTONDOWN,
			SDL_JOYBUTTONUP,
			SDL_JOYDEVICEADDED,
			SDL_JOYDEVICEREMOVED,

			/* Game controller events */
			SDL_CONTROLLERAXISMOTION = 	0x650,
			SDL_CONTROLLERBUTTONDOWN,
			SDL_CONTROLLERBUTTONUP,
			SDL_CONTROLLERDEVICEADDED,
			SDL_CONTROLLERDEVICEREMOVED,
			SDL_CONTROLLERDEVICEREMAPPED,

			/* Touch events */
			SDL_FINGERDOWN = 		0x700,
			SDL_FINGERUP,
			SDL_FINGERMOTION,

			/* Gesture events */
			SDL_DOLLARGESTURE =		0x800,
			SDL_DOLLARRECORD,
			SDL_MULTIGESTURE,

			/* Clipboard events */
			SDL_CLIPBOARDUPDATE =		0x900,

			/* Drag and drop events */
			SDL_DROPFILE =			0x1000,

			/* Events SDL_USEREVENT through SDL_LASTEVENT are for
			 * your use, and should be allocated with
			 * SDL_RegisterEvents()
			 */
			SDL_USEREVENT =			0x8000,

			/* The last event, used for bouding arrays. */
			SDL_LASTEVENT =			0xFFFF
		}

		/* Fields shared by every event */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_GenericEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
		}

		/* Window state change event data (event.window.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_WindowEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public SDL_WindowEventID windowEvent; // event, lolC#
			byte padding1;
			byte padding2;
			byte padding3;
			public Int32 data1;
			public Int32 data2;
		}
		
		/* Keyboard button event structure (event.key.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_KeyboardEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public byte state;
			public byte repeat; /* non-zero if this is a repeat */
			byte padding2;
			byte padding3;
			// TODO: SDL_Keysym struct.
		}

		//TODO: SDL_Text*Event (need to work out char[] in C#)

		/* Mouse motion event structure (event.motion.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_MouseMotionEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public UInt32 which;
			public byte state; /* bitmask of buttons */
			byte padding1;
			byte padding2;
			byte padding3;
			public Int32 x;
			public Int32 y;
			public Int32 xrel;
			public Int32 yrel;
		}

		/* Mouse button event structure (event.button.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_MouseButtonEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public UInt32 which;
			public byte button; /* button id */
			public byte state; /* SDL_PRESSED or SDL_RELEASED */
			byte padding1;
			byte padding2;
			public Int32 x;
			public Int32 y;
		}

		/* Mouse wheel event structure (event.wheel.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_MouseWheelEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public UInt32 which;
			public Int32 x; /* amount scrolled horizontally */
			public Int32 y; /* amount scrolled vertically */
		}

		/* Joystick axis motion event structure (event.jaxis.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyAxisEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte axis;
			byte padding1;
			byte padding2;
			byte padding3;
			public Int16 axisValue; /* value, lolC# */
			public UInt16 padding4;
		}

		/* Joystick trackball motion event structure (event.jball.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyBallEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte ball;
			byte padding1;
			byte padding2;
			byte padding3;
			public Int16 xrel;
			public Int16 yrel;
		}

		/* Joystick hat position change event struct (event.jhat.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyHatEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte hat; /* index of the hat */
			public byte hatValue; /* value, lolC# */
			byte padding1;
			byte padding2;
		}

		/* Joystick button event structure (event.jbutton.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyButtonEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte button;
			public byte state; /* SDL_PRESSED or SDL_RELEASED */
			byte padding1;
			byte padding2;
		}

		/* Joystick device event structure (event.jdevice.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_JoyDeviceEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
		}

		/* Game controller axis motion event (event.caxis.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_ControllerAxisEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte axis;
			byte padding1;
			byte padding2;
			byte padding3;
			public Int16 axisValue; /* value, lolC# */
			UInt16 padding4;
		}

		/* Game controller button event (event.cbutton.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_ControllerButtonEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* SDL_JoystickID */
			public byte button;
			public byte state;
			byte padding1;
			byte padding2;
		}

		/* Game controller device event (event.cdevice.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_ControllerDeviceEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public Int32 which; /* joystick id for ADDED, else
					       instance id */
		}


		// TODO: Touch Finger events, Gesture Events

		/* File open request by system (event.drop.*), disabled by
		 * default
		 */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_DropEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public IntPtr file; /* char* filename, to be freed */
		}

		/* The "quit requested" event */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_QuitEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
		}

		/* A user defined event (event.user.*) */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_UserEvent
		{
			public UInt32 type;
			public UInt32 timestamp;
			public UInt32 windowID;
			public Int32 code;
			public IntPtr data1; /* user-defined */
			public IntPtr data2; /* user-defined */
		}

		/* A video driver dependent event (event.syswm.*), disabled */
		[StructLayout(LayoutKind.Sequential)]
		public struct SDL_SysWMEvent
		{
			public SDL_EventType type;
			public UInt32 timestamp;
			public IntPtr msg; /* SDL_SysWMmsg*, system-dependent*/
		}

		/* General event structure */
		// C# doesn't do unions, so we do this ugly thing. */
		[StructLayout(LayoutKind.Explicit)]
		public struct SDL_Event
		{
			[FieldOffset(0)]
			public SDL_EventType type;
			[FieldOffset(0)]
			public SDL_WindowEvent window;
			[FieldOffset(0)]
			public SDL_KeyboardEvent key;
			//TODO: TextEditingEvent, TextInputEvent
			[FieldOffset(0)]
			public SDL_MouseMotionEvent motion;
			[FieldOffset(0)]
			public SDL_MouseButtonEvent button;
			[FieldOffset(0)]
			public SDL_MouseWheelEvent wheel;
			[FieldOffset(0)]
			public SDL_JoyAxisEvent jaxis;
			[FieldOffset(0)]
			public SDL_JoyBallEvent jball;
			[FieldOffset(0)]
			public SDL_JoyHatEvent jhat;
			[FieldOffset(0)]
			public SDL_JoyButtonEvent jbutton;
			[FieldOffset(0)]
			public SDL_JoyDeviceEvent jdevice;
			[FieldOffset(0)]
			public SDL_ControllerAxisEvent caxis;
			[FieldOffset(0)]
			public SDL_ControllerButtonEvent cbutton;
			[FieldOffset(0)]
			public SDL_ControllerDeviceEvent cdevice;
			[FieldOffset(0)]
			public SDL_QuitEvent quit;
			[FieldOffset(0)]
			public SDL_UserEvent user;
			[FieldOffset(0)]
			public SDL_SysWMEvent syswm;
			//TODO: Touch, Gesture events
			[FieldOffset(0)]
			public SDL_DropEvent drop;
		}

		/* Pump the event loop, getting events from the input devices*/
		[DllImport(nativeLibName)]
		public static extern void SDL_PumpEvents();

		public enum SDL_eventaction
		{
			SDL_ADDEVENT,
			SDL_PEEKEVENT,
			SDL_GETEVENT
		}

		//TODO: SDL_PeepEvents

		/* Checks to see if certain events are in the event queue */
		[DllImport(nativeLibName)]
		public static extern SDL_bool SDL_HasEvent(SDL_EventType type);
		//TODO: SDL_HasEvents

		/* Clears events from the event queue */
		[DllImport(nativeLibName)]
		public static extern void SDL_FlushEvent(SDL_EventType type);
		//TODO: SDL_FlushEvents

		/* Polls for currently pending events */
		[DllImport(nativeLibName)]
		public static extern int SDL_PollEvent(out SDL_Event _event);

		/* Waits indefinitely for the next event */
		[DllImport(nativeLibName)]
		public static extern int SDL_WaitEvent(out SDL_Event _event);

		/* Waits until the specified timeout (in ms) for the next event
		 */
		[DllImport(nativeLibName)]
		public static extern int SDL_WaitEventTimeout(out SDL_Event _event, int timeout);
		
		/* Add an event to the event queue */
		[DllImport(nativeLibName)]
		public static extern int SDL_PushEvent(ref SDL_Event _event);

		//TODO: All of the event filter stuff.

		/* These are for SDL_EventState() */
		public const int SDL_QUERY = 		-1;
		public const int SDL_IGNORE = 		0;
		public const int SDL_DISABLE =		0;
		public const int SDL_ENABLE = 		1;

		/* This function allows you to enable/disable certain events */
		[DllImport(nativeLibName)]
		public static extern byte SDL_EventState(SDL_EventType type, int state);

		/* Get the state of an event */
		public static byte SDL_GetEventState(SDL_EventType type)
		{
			return SDL_EventState(type, SDL_QUERY);
		}

		/* Allocate a set of user-defined events */
		[DllImport(nativeLibName)]
		public static extern UInt32 SDL_RegisterEvents(int numevents);
		#endregion
		
		#region SDL_scancode.h

		/* Scancodes based off USB keyboard page (0x07) */
		public enum SDL_Scancode
		{
			SDL_SCANCODE_UNKNOWN = 0,
			
			SDL_SCANCODE_A = 4,
			SDL_SCANCODE_B = 5,
			SDL_SCANCODE_C = 6,
			SDL_SCANCODE_D = 7,
			SDL_SCANCODE_E = 8,
			SDL_SCANCODE_F = 9,
			SDL_SCANCODE_G = 10,
			SDL_SCANCODE_H = 11,
			SDL_SCANCODE_I = 12,
			SDL_SCANCODE_J = 13,
			SDL_SCANCODE_K = 14,
			SDL_SCANCODE_L = 15,
			SDL_SCANCODE_M = 16,
			SDL_SCANCODE_N = 17,
			SDL_SCANCODE_O = 18,
			SDL_SCANCODE_P = 19,
			SDL_SCANCODE_Q = 20,
			SDL_SCANCODE_R = 21,
			SDL_SCANCODE_S = 22,
			SDL_SCANCODE_T = 23,
			SDL_SCANCODE_U = 24,
			SDL_SCANCODE_V = 25,
			SDL_SCANCODE_W = 26,
			SDL_SCANCODE_X = 27,
			SDL_SCANCODE_Y = 28,
			SDL_SCANCODE_Z = 29,

			SDL_SCANCODE_1 = 30,
			SDL_SCANCODE_2 = 31,
			SDL_SCANCODE_3 = 32,
			SDL_SCANCODE_4 = 33,
			SDL_SCANCODE_5 = 34,
			SDL_SCANCODE_6 = 35,
			SDL_SCANCODE_7 = 36,
			SDL_SCANCODE_8 = 37,
			SDL_SCANCODE_9 = 38,
			SDL_SCANCODE_0 = 39,

			SDL_SCANCODE_RETURN = 40,
			SDL_SCANCODE_ESCAPE = 41,
			SDL_SCANCODE_BACKSPACE = 42,
			SDL_SCANCODE_TAB = 43,
			SDL_SCANCODE_SPACE = 44,

			SDL_SCANCODE_MINUS = 45,
			SDL_SCANCODE_EQUALS = 46,
			SDL_SCANCODE_LEFTBRACKET = 47,
			SDL_SCANCODE_RIGHTBRACKET = 48,
			SDL_SCANCODE_BACKSLASH = 49,
			SDL_SCANCODE_NONUSHASH = 50,
			SDL_SCANCODE_SEMICOLON = 51,
			SDL_SCANCODE_APOSTROPHE = 52,
			SDL_SCANCODE_GRAVE = 53,
			SDL_SCANCODE_COMMA = 54,
			SDL_SCANCODE_PERIOD = 55,
			SDL_SCANCODE_SLASH = 56,

			SDL_SCANCODE_CAPSLOCK = 57,

			SDL_SCANCODE_F1 = 58,
			SDL_SCANCODE_F2 = 59,
			SDL_SCANCODE_F3 = 60,
			SDL_SCANCODE_F4 = 61,
			SDL_SCANCODE_F5 = 62,
			SDL_SCANCODE_F6 = 63,
			SDL_SCANCODE_F7 = 64,
			SDL_SCANCODE_F8 = 65,
			SDL_SCANCODE_F9 = 66,
			SDL_SCANCODE_F10 = 67,
			SDL_SCANCODE_F11 = 68,
			SDL_SCANCODE_F12 = 69,

			SDL_SCANCODE_PRINTSCREEN = 70,
			SDL_SCANCODE_SCROLLLOCK = 71,
			SDL_SCANCODE_PAUSE = 72,
			SDL_SCANCODE_INSERT = 73,
			SDL_SCANCODE_HOME = 74,
			SDL_SCANCODE_PAGEUP = 75,
			SDL_SCANCODE_DELETE = 76,
			SDL_SCANCODE_END = 77,
			SDL_SCANCODE_PAGEDOWN = 78,
			SDL_SCANCODE_RIGHT = 79,
			SDL_SCANCODE_LEFT = 80,
			SDL_SCANCODE_DOWN = 81,
			SDL_SCANCODE_UP = 82,

			SDL_SCANCODE_NUMLOCKCLEAR = 83,
			SDL_SCANCODE_KP_DIVIDE = 84,
			SDL_SCANCODE_KP_MULTIPLY = 85,
			SDL_SCANCODE_KP_MINUS = 86,
			SDL_SCANCODE_KP_PLUS = 87,
			SDL_SCANCODE_KP_ENTER = 88,
			SDL_SCANCODE_KP_1 = 89,
			SDL_SCANCODE_KP_2 = 90,
			SDL_SCANCODE_KP_3 = 91,
			SDL_SCANCODE_KP_4 = 92,
			SDL_SCANCODE_KP_5 = 93,
			SDL_SCANCODE_KP_6 = 94,
			SDL_SCANCODE_KP_7 = 95,
			SDL_SCANCODE_KP_8 = 96,
			SDL_SCANCODE_KP_9 = 97,
			SDL_SCANCODE_KP_0 = 98,
			SDL_SCANCODE_KP_PERIOD = 99,

			SDL_SCANCODE_NONUSBACKSLASH = 100,
			SDL_SCANCODE_APPLICATION = 101,
			SDL_SCANCODE_POWER = 102,
			SDL_SCANCODE_KP_EQUALS = 103,
			SDL_SCANCODE_F13 = 104,
			SDL_SCANCODE_F14 = 105,
			SDL_SCANCODE_F15 = 106,
			SDL_SCANCODE_F16 = 107,
			SDL_SCANCODE_F17 = 108,
			SDL_SCANCODE_F18 = 109,
			SDL_SCANCODE_F19 = 110,
			SDL_SCANCODE_F20 = 111,
			SDL_SCANCODE_F21 = 112,
			SDL_SCANCODE_F22 = 113,
			SDL_SCANCODE_F23 = 114,
			SDL_SCANCODE_F24 = 115,
			SDL_SCANCODE_EXECUTE = 116,
			SDL_SCANCODE_HELP = 117,
			SDL_SCANCODE_MENU = 118,
			SDL_SCANCODE_SELECT = 119,
			SDL_SCANCODE_STOP = 120,
			SDL_SCANCODE_AGAIN = 121,
			SDL_SCANCODE_UNDO = 122,
			SDL_SCANCODE_CUT = 123,
			SDL_SCANCODE_COPY = 124,
			SDL_SCANCODE_PASTE = 125,
			SDL_SCANCODE_FIND = 126,
			SDL_SCANCODE_MUTE = 127,
			SDL_SCANCODE_VOLUMEUP = 128,
			SDL_SCANCODE_VOLUMEDOWN = 129,
			/* not sure whether there's a reason to enable these */
			/*     SDL_SCANCODE_LOCKINGCAPSLOCK = 130,  */
			/*     SDL_SCANCODE_LOCKINGNUMLOCK = 131, */
			/*     SDL_SCANCODE_LOCKINGSCROLLLOCK = 132, */
			SDL_SCANCODE_KP_COMMA = 133,
			SDL_SCANCODE_KP_EQUALSAS400 = 134,

			SDL_SCANCODE_INTERNATIONAL1 = 135,
			SDL_SCANCODE_INTERNATIONAL2 = 136,
			SDL_SCANCODE_INTERNATIONAL3 = 137,
			SDL_SCANCODE_INTERNATIONAL4 = 138,
			SDL_SCANCODE_INTERNATIONAL5 = 139,
			SDL_SCANCODE_INTERNATIONAL6 = 140,
			SDL_SCANCODE_INTERNATIONAL7 = 141,
			SDL_SCANCODE_INTERNATIONAL8 = 142,
			SDL_SCANCODE_INTERNATIONAL9 = 143,
			SDL_SCANCODE_LANG1 = 144,
			SDL_SCANCODE_LANG2 = 145,
			SDL_SCANCODE_LANG3 = 146,
			SDL_SCANCODE_LANG4 = 147,
			SDL_SCANCODE_LANG5 = 148,
			SDL_SCANCODE_LANG6 = 149,
			SDL_SCANCODE_LANG7 = 150,
			SDL_SCANCODE_LANG8 = 151,
			SDL_SCANCODE_LANG9 = 152,

			SDL_SCANCODE_ALTERASE = 153,
			SDL_SCANCODE_SYSREQ = 154,
			SDL_SCANCODE_CANCEL = 155,
			SDL_SCANCODE_CLEAR = 156,
			SDL_SCANCODE_PRIOR = 157,
			SDL_SCANCODE_RETURN2 = 158,
			SDL_SCANCODE_SEPARATOR = 159,
			SDL_SCANCODE_OUT = 160,
			SDL_SCANCODE_OPER = 161,
			SDL_SCANCODE_CLEARAGAIN = 162,
			SDL_SCANCODE_CRSEL = 163,
			SDL_SCANCODE_EXSEL = 164,

			SDL_SCANCODE_KP_00 = 176,
			SDL_SCANCODE_KP_000 = 177,
			SDL_SCANCODE_THOUSANDSSEPARATOR = 178,
			SDL_SCANCODE_DECIMALSEPARATOR = 179,
			SDL_SCANCODE_CURRENCYUNIT = 180,
			SDL_SCANCODE_CURRENCYSUBUNIT = 181,
			SDL_SCANCODE_KP_LEFTPAREN = 182,
			SDL_SCANCODE_KP_RIGHTPAREN = 183,
			SDL_SCANCODE_KP_LEFTBRACE = 184,
			SDL_SCANCODE_KP_RIGHTBRACE = 185,
			SDL_SCANCODE_KP_TAB = 186,
			SDL_SCANCODE_KP_BACKSPACE = 187,
			SDL_SCANCODE_KP_A = 188,
			SDL_SCANCODE_KP_B = 189,
			SDL_SCANCODE_KP_C = 190,
			SDL_SCANCODE_KP_D = 191,
			SDL_SCANCODE_KP_E = 192,
			SDL_SCANCODE_KP_F = 193,
			SDL_SCANCODE_KP_XOR = 194,
			SDL_SCANCODE_KP_POWER = 195,
			SDL_SCANCODE_KP_PERCENT = 196,
			SDL_SCANCODE_KP_LESS = 197,
			SDL_SCANCODE_KP_GREATER = 198,
			SDL_SCANCODE_KP_AMPERSAND = 199,
			SDL_SCANCODE_KP_DBLAMPERSAND = 200,
			SDL_SCANCODE_KP_VERTICALBAR = 201,
			SDL_SCANCODE_KP_DBLVERTICALBAR = 202,
			SDL_SCANCODE_KP_COLON = 203,
			SDL_SCANCODE_KP_HASH = 204,
			SDL_SCANCODE_KP_SPACE = 205,
			SDL_SCANCODE_KP_AT = 206,
			SDL_SCANCODE_KP_EXCLAM = 207,
			SDL_SCANCODE_KP_MEMSTORE = 208,
			SDL_SCANCODE_KP_MEMRECALL = 209,
			SDL_SCANCODE_KP_MEMCLEAR = 210,
			SDL_SCANCODE_KP_MEMADD = 211,
			SDL_SCANCODE_KP_MEMSUBTRACT = 212,
			SDL_SCANCODE_KP_MEMMULTIPLY = 213,
			SDL_SCANCODE_KP_MEMDIVIDE = 214,
			SDL_SCANCODE_KP_PLUSMINUS = 215,
			SDL_SCANCODE_KP_CLEAR = 216,
			SDL_SCANCODE_KP_CLEARENTRY = 217,
			SDL_SCANCODE_KP_BINARY = 218,
			SDL_SCANCODE_KP_OCTAL = 219,
			SDL_SCANCODE_KP_DECIMAL = 220,
			SDL_SCANCODE_KP_HEXADECIMAL = 221,

			SDL_SCANCODE_LCTRL = 224,
			SDL_SCANCODE_LSHIFT = 225,
			SDL_SCANCODE_LALT = 226,
			SDL_SCANCODE_LGUI = 227,
			SDL_SCANCODE_RCTRL = 228,
			SDL_SCANCODE_RSHIFT = 229,
			SDL_SCANCODE_RALT = 230,
			SDL_SCANCODE_RGUI = 231,

			SDL_SCANCODE_MODE = 257,
			
			/* These come from the USB consumer page (0x0C) */	
			SDL_SCANCODE_AUDIONEXT = 258,
			SDL_SCANCODE_AUDIOPREV = 259,
			SDL_SCANCODE_AUDIOSTOP = 260,
			SDL_SCANCODE_AUDIOPLAY = 261,
			SDL_SCANCODE_AUDIOMUTE = 262,
			SDL_SCANCODE_MEDIASELECT = 263,
			SDL_SCANCODE_WWW = 264,
			SDL_SCANCODE_MAIL = 265,
			SDL_SCANCODE_CALCULATOR = 266,
			SDL_SCANCODE_COMPUTER = 267,
			SDL_SCANCODE_AC_SEARCH = 268,
			SDL_SCANCODE_AC_HOME = 269,
			SDL_SCANCODE_AC_BACK = 270,
			SDL_SCANCODE_AC_FORWARD = 271,
			SDL_SCANCODE_AC_STOP = 272,
			SDL_SCANCODE_AC_REFRESH = 273,
			SDL_SCANCODE_AC_BOOKMARKS = 274,

			/* These come from other sources, and are mostly mac related */
			SDL_SCANCODE_BRIGHTNESSDOWN = 275,
			SDL_SCANCODE_BRIGHTNESSUP = 276,
			SDL_SCANCODE_DISPLAYSWITCH = 277,
			SDL_SCANCODE_KBDILLUMTOGGLE = 278,
			SDL_SCANCODE_KBDILLUMDOWN = 279,
			SDL_SCANCODE_KBDILLUMUP = 280,
			SDL_SCANCODE_EJECT = 281,
			SDL_SCANCODE_SLEEP = 282,

			SDL_SCANCODE_APP1 = 283,
			SDL_SCANCODE_APP2 = 284,

			/* This is not a key, simply marks the number of scancodes
			 * so that you know how big to make your arrays. */
			SDL_NUM_SCANCODES = 512
		}

		#endregion

		/* TODO: Force Feedback:
		 * http://wiki.libsdl.org/moin.fcg/APIByCategory#Force_Feedback
		 */
		
		/* TODO: Audio:
		 * http://wiki.libsdl.org/moin.fcg/APIByCategory#Audio
		 */
	}
}

#pragma warning restore 0169
