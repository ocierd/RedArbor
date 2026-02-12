import { Component } from "@angular/core";

export interface GridColumn {
    field: string;
    header: string;
    sortable?: boolean;
    filterable?: boolean;
    width?: string;
    align?: 'left' | 'center' | 'right';
    component?: Component;
}