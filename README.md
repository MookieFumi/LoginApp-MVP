# LoginApp

¿Qué tiene "interesante" este repo?

* Patrón ViewHolder en el RecyclerView.

Es una forma de reutilizar vistas en el uso de listas, fundamentalmente ya que evitamos el uso de FindViewBy que es bastante costoso. Es decir, es un patrón para mejorar el rendimiento.

> Mantenemos una referencia a los elementos de la lista mientras se hace scroll.

* **Patrón MVP** Android

> Model View Presenter. Es un patrón utilizado para organizar nuestra capa de presentación. 

**Presenter**. Tiene la lógica de negocio de la UI, todas las interacciones de con la vista se delegan directamente en él. Por lo que veremos mucho código del estilo:

```csharp
void ButtonSaveClick()
    _presenter.Save()
```

Un presenter casi nunca se reutiliza ya que acaba siendo muy dependiente de una vista con la que interactua.

El Presenter es independiente del framework, en este caso Android.

**View**. La vista tiene la responsabilidad de renderizar la interfaz de usuario y de comunicar al presenter las interacciones del usuario. Las vistas son Activities, Fragments o Views...

```csharp
public class LoginActivity, LoginView {
    private LoginPresenter presenter;

    protected void onCreate(Bundle savedInstanceState) {
         presenter = new LoginPresenter(this);
    }
}
```

**Model**. El modelo son los datos, el estado y la lógica de negocio de nuestra aplicación.

* **Testing unitario** del Presenter. Al estar el Presenter independizado del framework (Android), incluso movido a otro ensamblado, el testing unitario se ve favorecido.

```csharp
[Fact]
public void Not_Allowed_Login_Calls_ShowUserIsNotAllowed_Method()
{
    _service.Setup(a => a.Login(GetValidUser().Key, GetValidUser().Value)).Returns(Task.FromResult(false));

    _sut.Login(GetValidUser().Key, GetValidUser().Value);

    _view.Verify(m => m.ShowUserIsNotAllowed(), Times.Once);
}
```

* En el diseño de layout se ha utilizado Tools para poder tener una **previsualización de nuestra lista, y sus elementos** en tiempo de diseño. ¿Cómo se consigue esto?

```xml
<LinearLayout xmlns:android="http://schemas.android.com/apk/res/android"
    xmlns:tools="http://schemas.android.com/tools">
    <android.support.v7.widget.RecyclerView
        android:id="@+id/lastUsersRecyclerView"
        android:scrollbars="vertical"
        android:layout_width="match_parent"
        android:layout_height="wrap_content"
        tools:layoutManager="android.support.v7.widget.GridLayoutManager"
        tools:listitem="@layout/LastUserView"
        tools:orientation="horizontal"
        tools:scrollbars="horizontal" />
</LinearLayout>
```

Referencia en [Android Developer](https://developer.android.com/studio/write/tool-attributes.html)