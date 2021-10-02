#include <stdio.h>
#include <stdlib.h>

#define days 31
int pressure[31];

#define p_min 740
#define p_max 780
#define p_norm 750

int main()
{

    for (int i = 0; i < days; i++){
        pressure[i] = p_min + rand() % (p_max - p_min + 1);
    }

    for (int i = 0; i < days; i++) {
        printf("day: %d, presure: %d\n", i +1, pressure[i]);
    }

    int cnt_below = 0;
    int cnt_above = 0;
    int cnt_exact = 0;

    for (int i = 0; i < days; i++) {
        int v = pressure[i];
        if (v < p_norm) {
            cnt_below++;
        } else if (v > p_norm) {
            cnt_above++;
        }
        else {
            cnt_exact++;
        }
    }

    printf("below: %d\n", cnt_below);
    printf("above: %d\n", cnt_above);
    printf("exact: %d\n", cnt_exact);

    return 0;
}