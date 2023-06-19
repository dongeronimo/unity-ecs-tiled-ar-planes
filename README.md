I'm studying how to use Unity's DOTS system because I want 10k character controllers and 100k objects in the scene.

Unity Version: 2022.3.0f1

Packages to add:

 com.unity.burst 1.8.2
 com.unity.collections 2.1.4
 com.unity.entities 1.0.10
 com.unity.entities.graphics 1.0.10
 com.unity.mathematics 1.2.6

Just add them to the manifest.json.

Render Pipeline: URP

1) Preparing to develop:
    - Create a folder called HelloWorld.
    - Inside it create a scene called Hello.
    - In it's hierarchy create a new emoty sub scene called HelloRotatingCubesSubscene.
    The names are incidental, they could be called anything you want.
    There will be a folder inside HelloWorld and the scene will have a gameObject calle HelloRotatingCubesSubscene with the SubScene script, with it's Scene Asset property pointing to the subscene we created.

2) Subscene:
    The subscene is where we author the game using old-school game objects that will be converted to entities. The gameObjects aren't going nowhere and they will always be unity's authoring tool.
    - Create a cube, called MyCube. Delete the BoxCollider, it won't be used in this hello world.
    - Create a script, RotationSpeedAuthoring, and drag it to the cube.
    - A monoBehaviour that will be baked to the entity system should only have data. They are needed because you cant edit entity components values in the Editor. The way to author content is thru gameObjects that will be baked. So the RotationSpeedAuthoring will be like this:
    ```
    public class RotationSpeedAuthoring : MonoBehaviour
    {
        public float DegreesPerSecond = 360.0f;
    }
    ```
    I also created a struct descendant of IComponentData to hold the ECS component data:
    ```
    public struct RotationSpeed: IComponentData
    {
        public float RadiansPerSecond;
    }
    ```