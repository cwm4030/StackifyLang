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

```
use std.io

namespace example2

type Person ->
    :name
    :age

    fn init @n @a ->
        Person.new @p
        n @p.name
        a @p.age
        p
    end

    fn greet @p ->
        "Hello, my name is " p.name string.concat
        " and I am " p.age int.to_string string.concat
        " years old. It is nice to meet you." string.concat
        println
    end
end

"John Doe" 40 Person.init @p
p Person.greet
```

```
use std.io

namespace example3

fn guess_age @age ->
    0 @wrong
    while wrong 10 < then
        "Enter a guess for my age:" println
        readln int.parse @guess

        if guess age = then
            "Congrats, you guessed correct!" println
            break
        else
            wrong 1 + @wrong
        end
    end

    "You guessed wrong " wrong int.to_string string.concat
    " times." string.concat
    println
end

40 guess_age
```

Closures
```
use std.io

namespace example4

fn make_counter @start ->
    start @i
    [@inc -> i inc + @i i print]
end

2 make_counter @counter
1 count call // 3
4 count call // 7

// equavalent to the above code
fn make_counter1 @start ->
    start @i
    fn count @inc ->
        i inc + @i i print
    end
    // anon fn with no paramaters
    [:count]
end

2 make_counter1 @counter
1 count call // 3
4 count call // 7
```