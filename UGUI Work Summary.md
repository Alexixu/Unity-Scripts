# UGUI 小结
## 1. Image
### a. Image（以及RawImage之类）中，alpha值为0时，其子对象也会一起不显示。并且raycast无效；当alpha值为1时（range 0-255）,当前游戏对象不显示，但是，子对象会显示，不受影响，此时，raycast正常有效。
### b. 可以编写脚本改变Image形状，实现自定义形状；具体有两种实现方式，第一种，直接继承Image类或RawImage类，覆盖下面虚函数，但是不推荐；
    protected override void OnPopulateMesh(VertexHelper toFill)
### c. 第二种方式是在Image游戏对象上添加脚本；在Image的基类Graphic中：
    private void DoMeshGeneration()
    {
            if (rectTransform != null && rectTransform.rect.width >= 0 && rectTransform.rect.height >= 0)
                OnPopulateMesh(s_VertexHelper);
            else
                s_VertexHelper.Clear(); // clear the vertex helper so invalid graphics dont draw.

            var components = ListPool<Component>.Get();
            GetComponents(typeof(IMeshModifier), components);

            for (var i = 0; i < components.Count; i++)
                ((IMeshModifier)components[i]).ModifyMesh(s_VertexHelper);

            ListPool<Component>.Release(components);

            s_VertexHelper.FillMesh(workerMesh);
            canvasRenderer.SetMesh(workerMesh);
    }
### 可以看到，调用虚函数OnPopulateMesh（VertexHelper）后，还会将该对象上带有IMeshModifier接口的脚本提取出来，按先后顺序执行。所以，可以编写实现了IMeshModifier接口的类来完成Image形状的改变。具体代码可以参考OnPopulateMesh（VertexHelper）中是如何绘制图形代码。
    



