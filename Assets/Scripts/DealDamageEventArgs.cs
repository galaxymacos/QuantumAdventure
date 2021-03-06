﻿using System;
using System.Linq;
using Photon.Pun;
using UnityEngine;

public class DealDamageEventArgs : EventArgs
{
    public int TargetViewId;

    public int DamageOwnerViewId;

    public Vector3 DamageOwnerPosition;

    public int DamageAmount;

    public int AngerAmount;

    /// <summary>
    /// How much force to launch the player onto the air
    /// </summary>
    public float VerticalForce;

    /// <summary>
    /// How many force to push the player away from the damage dealer
    /// </summary>
    public float HorizontalForce;

    // public static byte[] Serialize(object obj)
    // {
    //     DealDamageEventArgs data = (DealDamageEventArgs) obj;
    //
    //     // Serialization
    //
    //     // int
    //     byte[] targetViewIdBytes = BitConverter.GetBytes(data.targetViewID);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(targetViewIdBytes);
    //     }
    //
    //     byte[] damageOwnerViewId = BitConverter.GetBytes(data.damageOwnerViewID);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageOwnerViewId);
    //     }
    //
    //     byte[] damageBytes = BitConverter.GetBytes(data.damageAmount);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageBytes);
    //     }
    //
    //     // Vector 3
    //     byte[] damageOwnerPositionXBytes = BitConverter.GetBytes(data.damageOwnerPosition.x);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageOwnerPositionXBytes);
    //     }
    //
    //     byte[] damageOwnerPositionYBytes = BitConverter.GetBytes(data.damageOwnerPosition.y);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageOwnerPositionYBytes);
    //     }
    //
    //     byte[] damageOwnerPositionZBytes = BitConverter.GetBytes(data.damageOwnerPosition.z);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageOwnerPositionZBytes);
    //     }
    //
    //     byte[] takeDownValueBytes = BitConverter.GetBytes(data.takeDownValue);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(takeDownValueBytes);
    //     }
    //
    //     byte[] verticalForce = BitConverter.GetBytes(data.verticalForce);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(verticalForce);
    //     }
    //
    //     byte[] horizontalForce = BitConverter.GetBytes(data.horizontalForce);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(horizontalForce);
    //     }
    //
    //     byte[] angerAmountBytes = BitConverter.GetBytes(data.angerAmount);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(angerAmountBytes);
    //     }
    //
    //     return JoinBytes(targetViewIdBytes, damageOwnerViewId, damageOwnerPositionXBytes, damageOwnerPositionYBytes,
    //         damageOwnerPositionZBytes, takeDownValueBytes, verticalForce, horizontalForce, damageBytes, angerAmountBytes);
    // }

    // private static byte[] JoinBytes(params byte[][] bytesArray)
    // {
    //     byte[] rv = new byte[bytesArray.Sum(x => x.Length)];
    //     int offset = 0;
    //     foreach (byte[] array in bytesArray)
    //     {
    //         Buffer.BlockCopy(array, 0, rv, offset, array.Length);
    //         offset += array.Length;
    //     }
    //
    //     return rv;
    // }

    // public static object Deserialize(byte[] bytes)
    // {
    //     DealDamageEventArgs data = new DealDamageEventArgs();
    //
    //     byte[] targetViewIdBytes = new byte[4];
    //     Array.Copy(bytes, 0, targetViewIdBytes, 0, targetViewIdBytes.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(targetViewIdBytes);
    //     }
    //
    //     data.targetViewID = BitConverter.ToInt32(targetViewIdBytes, 0);
    //
    //     byte[] damageOwnerViewId = new byte[4];
    //     Array.Copy(bytes, 0, targetViewIdBytes, 0, targetViewIdBytes.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageOwnerViewId);
    //     }
    //
    //     data.damageOwnerViewID = BitConverter.ToInt32(damageOwnerViewId, 0);
    //
    //     byte[] damageOwnerPositionX = new byte[4];
    //     Array.Copy(bytes, 0, damageOwnerPositionX, 0, damageOwnerPositionX.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageOwnerPositionX);
    //     }
    //
    //     var xValue = BitConverter.ToDouble(damageOwnerPositionX, 0);
    //
    //     byte[] damageOwnerPositionY = new byte[4];
    //     Array.Copy(bytes, 0, damageOwnerPositionY, 0, damageOwnerPositionY.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageOwnerPositionY);
    //     }
    //
    //     var yValue = BitConverter.ToDouble(damageOwnerPositionY, 0);
    //
    //     byte[] damageOwnerPositionZ = new byte[4];
    //     Array.Copy(bytes, 0, damageOwnerPositionZ, 0, damageOwnerPositionZ.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(damageOwnerPositionZ);
    //     }
    //
    //     var zValue = BitConverter.ToDouble(damageOwnerPositionZ, 0);
    //
    //     data.damageOwnerPosition = new Vector3((float) xValue, (float) yValue, (float) zValue);
    //
    //     byte[] takeDownValueBytes = new byte[4];
    //     Array.Copy(bytes, 0, takeDownValueBytes, 0, takeDownValueBytes.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(takeDownValueBytes);
    //     }
    //
    //     data.takeDownValue = BitConverter.ToInt32(takeDownValueBytes, 0);
    //
    //     byte[] horizontalForce = new byte[4];
    //     Array.Copy(bytes, 0, horizontalForce, 0, horizontalForce.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(horizontalForce);
    //     }
    //
    //     data.horizontalForce = (float) BitConverter.ToDouble(horizontalForce, 0);
    //
    //     byte[] verticalForce = new byte[4];
    //     Array.Copy(bytes, 0, verticalForce, 0, verticalForce.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(verticalForce);
    //     }
    //
    //     data.verticalForce = (float) BitConverter.ToDouble(verticalForce, 0);
    //
    //     byte[] damageBytes = new byte[4];
    //     Array.Copy(bytes, 0, damageBytes, 0, verticalForce.Length);
    //     if (BitConverter.IsLittleEndian)
    //     {
    //         Array.Reverse(verticalForce);
    //     }
    //
    //
    //     return data;
    // }
}