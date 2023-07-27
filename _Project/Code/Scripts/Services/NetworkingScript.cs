using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Netcode;
using Unity.Collections;

#region NEW_TYPE_STRUCT_FOR_NETWORK
public struct MyType : INetworkSerializable
{
    public int _int;
    public bool _bool;
    public FixedString128Bytes _string;
    public float _float;

    public void NetworkSerialize<T>(BufferSerializer<T> serializer) where T : IReaderWriter
    {
        serializer.SerializeValue(ref _int);
        serializer.SerializeValue(ref _bool);
        serializer.SerializeValue(ref _string);
        serializer.SerializeValue(ref _float);
    }
}
#endregion


public class NetworkingScript : NetworkBehaviour
{
    #region ADD_NEW_VARIABLE_INTEGER_TYPE_FOR_NETWORK
    private NetworkVariable<int> Number = new NetworkVariable<int>
        (0, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Owner);
    #endregion

    #region ADD_NEW_VARIBALE_NEWTYPE_TYPE_FOR_NETWORK
    private NetworkVariable<MyType> myValue = new NetworkVariable<MyType>(
        new MyType { _int = 1, _bool = false, _string = "", _float = 23.0f }
        , NetworkVariableReadPermission.Everyone
        , NetworkVariableWritePermission.Owner);
    #endregion

    #region RUN_FUNCTION_AFTER_SPAWN_IN_NETWORK
    public override void OnNetworkSpawn()
    {
        print("Spawning...");

        Number.OnValueChanged += (int previousValue, int newValue) =>
        {
            print("Player Number [ " + OwnerClientId + "] - Int Value : " + Number.Value);
        };

        myValue.OnValueChanged += (MyType previousValue, MyType newValue) =>
        {
            print("Player Number [ " + OwnerClientId + "] - All Values : " +
                newValue._bool +" - "+ newValue._float + " - " + newValue._int + " - " + newValue._string);
        };
    }
    #endregion

    [ServerRpc]
    void TestServerRpc(ServerRpcParams serverRpcParams)
    {
        Debug.Log("Hi Server Remote Procedure Call (RPC) : " +
            OwnerClientId + " : "+ serverRpcParams.Receive.SenderClientId) ;
    }

    [ClientRpc]
    void TestClientRpc(ClientRpcParams clientRpcParams)
    {
        Debug.Log("Hi Client Remote Procedure Call (RPC) : " + clientRpcParams.Send.TargetClientIds);
    }

    private void Update()
    {
        if (!IsOwner) return;
    
        if (Input.GetKeyDown(KeyCode.B))
        {
            Number.Value += 2;
        }
        if (Input.GetKeyDown(KeyCode.N))
        {
            myValue.Value = new MyType
            {
                _bool = false,
                _float = 10f,
                _int = 2,
                _string = "Test MyType"            
            };
        }

        if (Input.GetKeyDown(KeyCode.J))
        {
            TestClientRpc(new ClientRpcParams {
                Send = new ClientRpcSendParams { TargetClientIds = new List<ulong> { 1 } }
            });

        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            TestServerRpc(new ServerRpcParams());
        }
    }
}
