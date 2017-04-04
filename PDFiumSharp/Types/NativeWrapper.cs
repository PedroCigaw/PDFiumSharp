﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace PDFiumSharp.Types
{
    public abstract class NativeWrapper<T> : IDisposable
		where T : struct, IHandle<T>
    {
		readonly T _handle;

		/// <summary>
		/// Handle which can be used with the native <see cref="PDFium"/> functions.
		/// </summary>
		public T Handle
		{
			get
			{
				if (_handle.IsNull)
					throw new ObjectDisposedException(GetType().FullName);
				return _handle;
			}
		}

		/// <summary>
		/// Gets a value indicating whether <see cref="IDisposable.Dispose"/> was already
		/// called on this instance.
		/// </summary>
		public bool IsDisposed => _handle.IsNull;

		protected NativeWrapper(T handle)
		{
			_handle = handle;
		}

		/// <summary>
		/// Implementors should clean up here. This method is guaranteed to only be called once.
		/// </summary>
		protected abstract void Dispose(T handle);

		void IDisposable.Dispose()
		{
			var oldHandle = _handle.SetToNull();
			if (!oldHandle.IsNull)
				Dispose(oldHandle);
		}
	}
}