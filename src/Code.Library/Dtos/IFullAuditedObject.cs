﻿using Code.Library.Auditing;

namespace Code.Library.Dtos
{
    /// <summary>
    /// This interface adds <see cref="IDeletionAuditedObject"/> to <see cref="IAuditedObject"/>.
    /// </summary>
    public interface IFullAuditedObject : IAuditedObject, IDeletionAuditedObject
    {
    }

    /// <summary>
    /// Adds user navigation properties to <see cref="IFullAuditedObject"/> interface for user.
    /// </summary>
    /// <typeparam name="TUser">Type of the user</typeparam>
    public interface IFullAuditedObject<TUser> : IAuditedObject<TUser>, IFullAuditedObject, IDeletionAuditedObject<TUser>
    {
    }
}