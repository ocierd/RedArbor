import { NgModule } from "@angular/core";
import { MaterialModule } from "./material/material-module";
import { FormField } from "@angular/forms/signals";
import { ComponentsModule } from "./components/components-module";
import { PipesModule } from "./pipes/pipes-module";
import { RedarborProviders } from "./providers/redarbor-providers";

/**
 * Shared module that contains common components, pipes, and services used across the application
 * This module should be imported in any feature module that needs access to these shared resources
 */
const sharedModules = [
    MaterialModule,
    ComponentsModule,
    PipesModule,

    FormField
];


@NgModule({
    declarations: [
        
  ],
    imports: [

        ...sharedModules
    ],
    exports: [
        ...sharedModules
    ],

    providers:[
        ...RedarborProviders
    ]
})
export class SharedModule {


}