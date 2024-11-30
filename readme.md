# Stackify
## Work in progress scripting language
Stackify is an imperative, concatenative scripting language with variables and a familiar c-like control flow.
<br>
**Currently the language is in a non-functional state. Hope that changes soon :)**

## Example Code
```
use std.io

namespace example1

fn nth_fibonacci @n ->
    if n 1 <= then
        0
    else
        n 1 - nth_fibonacci n 2 - nth_fibonacci +
    end
end

fn square @x ->
    x x *
end

4 square nth_fibonacci int.to_string print
```