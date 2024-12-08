# Stackify Grammar
* **literal_stmt** -> INTEGER | FLOAT | STRING | 'true' | 'false' | 'nil'
* **op_stmt** -> '+' | '-' | '*' | '/' | '~' | '<' | '>' | '=' | '!' | '!=' | '<=' | '>=' | 'or' | 'and' | 'dup' | 'swap' | 'drop' | 'call' | 'print' | 'println' | 'read' | 'readln'
* **identifier_stmt** -> (IDENTIFIER ('.' IDENTIFIER)*)
* **ret_stmt** -> 'ret'
* **break_stmt** -> 'break'
* **variable_stmt** -> '@' IDENTIFIER
* **property_stmt** -> ':' IDENTIFIER
* **stmt** -> literal_stmt | op_stmt | identifier_stmt | variable_stmt | if_stmt | while_stmt | anon_fn_stmt
* **branch_stmt** -> (literal_stmt | op_stmt | identifier_stmt | anon_fn_stmt)*
* **if_stmt** -> 'if' branch_stmt 'then' stmt* ('elif' branch_stmt 'then' stmt*)* ('else' stmt*)? 'end'
* **while_stmt** -> 'while' branch_stmt 'then' (stmt | break_stmt)* 'end'
* **fn_stmt** -> 'fn' IDENTIFIER variable_stmt* '->' (stmt | ret_stmt)* 'end'
* **anon_fn_stmt** -> '[' (variable_stmt* '->' | ':') (stmt | ret_stmt)* ']'
* **type_stmt** -> 'type' IDENTIFIER '->' (property_stmt | fn_stmt)* 'end'
* **use_stmt** -> 'use' identifier_stmt
* **namespace_stmt** -> 'namespace' identifier_stmt
* **program** -> use_stmt* namespace_stmt? (fn_stmt | type_stmt | stmt)*