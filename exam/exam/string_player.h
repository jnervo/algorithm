#include <iostream>
#include <vector>
#include <string.h>

// find t in s, start from start_index, return index
int str_find(const char *s, const char *t, const int start_index);

// reverse string
void str_reverse(char* str);

// get a new string equals str_reverse(str)
char* get_str_reverse(const char *str);

// if can find strA and strB one by one, in fullStr
bool can_find(char* fullStr, char* strA, char* strB);

// 360 exam, return which direction can find strA and strB one by one, in fullStr
char* which_direction(char* fullStr, char* strA, char* strB);