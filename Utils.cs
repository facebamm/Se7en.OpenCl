﻿using System;
using System.Runtime.CompilerServices;

namespace Se7en.OpenCl
{
    internal unsafe delegate ErrorCode GetInfoHandler<TIn, TInfo>(TIn handle, TInfo info, uint paramValSize, void* paramVal, out uint paramValSizeRet);
    internal static class Utils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe static TOut[] GetTInfo<TInfo, TIn, TOut>(TIn handle, TInfo info, GetInfoHandler<TIn, TInfo> getInfoHandler, out uint length)
           where TInfo : Enum
           where TIn : unmanaged
           where TOut : unmanaged
        {
            ErrorCode err;
            if ((err = getInfoHandler(handle, info, 0, null, out length)) == ErrorCode.Success)
            {
                TOut[] target = new TOut[length];
                fixed (TOut* targetPtr = target)
                {
                    if ((err = getInfoHandler(handle, info, length, targetPtr, out _)) == ErrorCode.Success)
                    {
                        return target;
                    }
                }
            }
            throw new Exception($"{err}");
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal unsafe static TOut GetTInfo<TInfo, TIn, TOut>(TIn handle, TInfo info, GetInfoHandler<TIn, TInfo> getInfoHandler)
          where TInfo : Enum
          where TOut : unmanaged
        {
            ErrorCode err;
            if ((err = getInfoHandler(handle, info, 0, null, out uint length)) == ErrorCode.Success)
            {
                TOut @out/* = default*/;
                if ((err = getInfoHandler(handle, info, length, &@out, out _)) == ErrorCode.Success)
                {
                    return @out;
                }
            }
            throw new Exception($"{err}");
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public unsafe static string ToStrg(this byte[] source)
        {
            fixed (byte* sourcePtr = source)
            {
                return new string((sbyte*)sourcePtr).TrimEnd('\0');
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static unsafe T First<T>(this T[] source, Func<T, bool> comp) where T : unmanaged
        {
            fixed (T* sourcePtr = source)
            {
                for (int i = 0, n = source.Length; i < n; i++)
                {
                    T* obj = sourcePtr + i;
                    if (comp(*obj))
                    {
                        return *sourcePtr;
                    }
                }
                return default;
            }
        }
    }
}
