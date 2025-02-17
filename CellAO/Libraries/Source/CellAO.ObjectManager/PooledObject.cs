﻿#region License

// Copyright (c) 2005-2014, CellAO Team
// 
// 
// All rights reserved.
// 
// 
// Redistribution and use in source and binary forms, with or without modification, are permitted provided that the following conditions are met:
// 
// 
//     * Redistributions of source code must retain the above copyright notice, this list of conditions and the following disclaimer.
//     * Redistributions in binary form must reproduce the above copyright notice, this list of conditions and the following disclaimer in the documentation and/or other materials provided with the distribution.
//     * Neither the name of the CellAO Team nor the names of its contributors may be used to endorse or promote products derived from this software without specific prior written permission.
// 
// 
// THIS SOFTWARE IS PROVIDED BY THE COPYRIGHT HOLDERS AND CONTRIBUTORS
// "AS IS" AND ANY EXPRESS OR IMPLIED WARRANTIES, INCLUDING, BUT NOT
// LIMITED TO, THE IMPLIED WARRANTIES OF MERCHANTABILITY AND FITNESS FOR
// A PARTICULAR PURPOSE ARE DISCLAIMED. IN NO EVENT SHALL THE COPYRIGHT OWNER OR
// CONTRIBUTORS BE LIABLE FOR ANY DIRECT, INDIRECT, INCIDENTAL, SPECIAL,
// EXEMPLARY, OR CONSEQUENTIAL DAMAGES (INCLUDING, BUT NOT LIMITED TO,
// PROCUREMENT OF SUBSTITUTE GOODS OR SERVICES; LOSS OF USE, DATA, OR
// PROFITS; OR BUSINESS INTERRUPTION) HOWEVER CAUSED AND ON ANY THEORY OF
// LIABILITY, WHETHER IN CONTRACT, STRICT LIABILITY, OR TORT (INCLUDING
// NEGLIGENCE OR OTHERWISE) ARISING IN ANY WAY OUT OF THE USE OF THIS
// SOFTWARE, EVEN IF ADVISED OF THE POSSIBILITY OF SUCH DAMAGE.
// 

#endregion

namespace CellAO.ObjectManager
{
    #region Usings ...

    using System;

    using CellAO.Interfaces;

    using SmokeLounge.AOtomation.Messaging.GameData;

    using Utility;

    #endregion

    /// <summary>
    /// </summary>
    public class PooledObject : IDisposable, IEntity, IPooledObject
    {
        private bool disposed = false;

        /// <summary>
        /// </summary>
        /// <param name="pooledIn">
        /// </param>
        /// <param name="parent">
        /// </param>
        /// <param name="id">
        /// </param>
        public PooledObject(Identity parent, Identity id)
        {
            this.Identity = id;
            this.Parent = parent;
            Pool.Instance.AddObject(parent, this);
            LogUtil.Debug(
                DebugInfoDetail.Pool,
                "Created new object " + id.ToString() + " of " + parent.ToString());
        }

        /// <summary>
        /// </summary>
        public Identity Identity { get; private set; }

        public virtual Identity Parent { get; private set; }

        /// <summary>
        /// </summary>
        public void Dispose()
        {
            LogUtil.Debug(DebugInfoDetail.Pool, "Removed object " + this.Identity.ToString() + " of " + this.Parent.ToString());
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (!this.disposed)
                {
                    Pool.Instance.RemoveObject(this);
                }
            }
            this.disposed = true;
        }
    }
}