import { Component, PipeTransform, Type } from "@angular/core";

/**
 * Interfaces related to the configuration of a grid component, 
 * such as columns and pipes to apply to the data displayed in the grid
 */
export interface GridColumn {
    field: string;
    header: string;
    sortable?: boolean;
    filterable?: boolean;
    width?: string;
    align?: 'left' | 'center' | 'right';
    component?: Component;
    pipe?: PipeType
}

/**
 * Interface that defines the type of a pipe to apply to a column in the grid, 
 * including the type of the pipe and the arguments to pass to the pipe
 */
export interface PipeType {
    type: Type<PipeTransform>;
    args?: any | any[];
}