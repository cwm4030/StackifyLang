# Stackify Grammar
* **literal** -> INTEGER | FLOAT | STRING | 'true' | 'false' | 'nil'
* **var_def** -> '@' IDENTIFIER
* **prop_def** -> ':' IDENTIFIER
* **identifier_stmt** -> IDENTIFIER ('.' IDENTIFIER)*
* **use_stmt** -> 'use' identifier_stmt
* **namespace_stmt** -> 'namespace' identifier_stmt
* **stmt** -> '+' | '-' | '*' | '/' | '<' | '>' | '=' | '!' | '!=' | '<=' | '>=' | literal | identifier_stmt
            | var_def | 'ret' | 'print' | 'println' | 'read' | 'readln' | 'call' | 'quote' | 'dup' | 'swap' | 'concat' | 'drop'
            | anon_fn | if_stmt | while_stmt
* **block_stmt** -> stmt*
* **anon_fn** -> '[' (var_def* '->' | ':') block_stmt ']'
* **if_stmt** -> 'if' block_stmt 'then' block_stmt ('elif' block_stmt 'then' block_stmt)* ('else' block_stmt)? 'end'
* **while_stmt** -> 'while' block_stmt 'then' block_stmt 'end'
* **fn_def** -> 'fn' IDENTIFIER var_def* '->' block_stmt 'end'
* **type_def** -> 'type' IDENTIFIER '->' (prop_def | fn_def)* 'end'
* **program** -> use_stmt* namespace_stmt? (fn_def | type_def | stmt)*