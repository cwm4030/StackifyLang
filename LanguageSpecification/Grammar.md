# Stackify Grammar
* **literal** -> INTEGER | FLOAT | STRING | 'true' | 'false' | 'nil'
* **var_def** -> '@' IDENTIFIER
* **prop_def** -> ':' IDENTIFIER
* **identifier_stmt** -> IDENTIFIER ('.' IDENTIFIER)*
* **use_stmt** -> 'use' identifier_stmt
* **namespace_stmt** -> 'namespace' identifier_stmt
* **stmt** -> '+' | '-' | '*' | '/' | '<' | '>' | '=' | '!' | '!=' | '<=' | '>=' | literal | identifier_stmt
            | var_def | 'ret' | 'print' | 'println' | 'read' | 'readln' | 'call' | 'quote' | 'dup' | 'swap' | 'concat' | 'drop'
            | anon_fn | if_stmt | while_stmt | use_stmt | namespace_stmt
* **anon_fn** -> '[' (var_def* '->' | ':') stmt* ']'
* **if_stmt** -> 'if' stmt* 'then' stmt* ('elif' stmt* 'then' stmt*)* ('else' stmt*)? 'end'
* **while_stmt** -> 'while' stmt* 'then' stmt* 'end'
* **fn_def** -> 'fn' IDENTIFIER var_def* '->' stmt* 'end'
* **type_def** -> 'type' IDENTIFIER '->' (prop_def | fn_def)* 'end'
* **program** -> (fn_def | type_def | stmt)*