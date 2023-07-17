using UnityEngine;

[CreateAssetMenu(fileName = "Domain", menuName = "Data/Domain")]
public class DomainContainerSO : ScriptableObject
{
    [field: SerializeField]
    public string Domain { get; private set; }
}
