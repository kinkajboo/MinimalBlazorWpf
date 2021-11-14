using Microsoft.AspNetCore.Components;

namespace BlazorLib
{
    public partial class Component1 : ComponentBase
    {
        protected int Counter = 0;
        protected void Increment()
        {
            Counter++;
        }
    }
}