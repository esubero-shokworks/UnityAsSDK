internal interface IARService
{
    void InstantiateARObject(string objectNameToInstantiate);

    void DestroyARObject(string objectNameToDestroy);

    void ChangeARCamera(string targetCamera);
}
