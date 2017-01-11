# CSF.Data.Entities
Right now this project only contains two extension methods for `IQuery<T>`.
These allow you to use the `Get` and `Theorise` method with generic `IIdentity<T>` instances, returning an appropriate entity type.
It also enables generic type inference (of the Get/Theorise result) from the identity generic type.

```csharp
private CSF.Data.IQuery _query;

Person GetPerson(CSF.Entities.IIdentity<Person> id)
{
  // The extension method will deal with the generics and null-handling for you.
  return _query.Get(id);
}
```

It's nothing spectacular but it cuts down on boilerplate generic parameters.

## Open source license
All source files within this project are released as open source software,
under the terms of [the MIT license].

[the MIT license]: http://opensource.org/licenses/MIT

This software is distributed in the hope that it will be useful, but please
remember that:

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN
THE SOFTWARE.