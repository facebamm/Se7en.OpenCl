using Se7en.OpenCl.Native;
using System;

namespace Se7en.OpenCl
{  
	[Flags]
    public enum SVMMemFlags 
    {
        /// <summary>
        /// This flag specifies that the SVM buffer will be read and written by a kernel. This is the default.
        /// </summary>
        ReadWrite = NativeCl.CL_MEM_READ_WRITE,
		/// <summary>
        /// This flag specifies that the SVM buffer will be written but not read by a kernel.<br/>
        /// Reading from a SVM buffer created with CL_MEM_WRITE_ONLY inside a kernel is undefined.<br/>
        /// ReadWrite and WriteOnly are mutually exclusive.
        /// </summary>
        WriteOnly = NativeCl.CL_MEM_WRITE_ONLY,
		/// <summary>
        /// This flag specifies that the SVM buffer object is a read-only memory object when used inside a kernel.<br/>
        /// Writing to a SVM buffer created with CL_MEM_READ_ONLY inside a kernel is undefined.<br/>
        /// ReadWrite or WriteOnly and ReadOnly are mutually exclusive.
        /// </summary>
        ReadOnly = NativeCl.CL_MEM_READ_ONLY,
		/// <summary>
        /// This specifies that the application wants the OpenCL implementation to do a fine-grained allocation.
        /// </summary>
        FineGrainBuffer = NativeCl.CL_MEM_SVM_FINE_GRAIN_BUFFER,
		/// <summary>
        /// This flag is valid only if CL_MEM_SVM_FINE_GRAIN_BUFFER is specified in flags.<br/>
        /// It is used to indicate that SVM atomic operations can control visibility of memory accesses in this SVM buffer.
        /// </summary>
        Atomic = NativeCl.CL_MEM_SVM_ATOMICS,
		
    };
}