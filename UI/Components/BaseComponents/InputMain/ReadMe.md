# `InputMain`

This is a base class for all input components to inherit from. When using this you can use `<InputContainer />` component to handle some of the basic parameters like a `Label` or `ErrorMessage`. If you would like to use this to make a custom input component, bind the html input to `mobjCurrentValue` like `<input type="text" @bind=@mobjCurrentValue />`

> ![Note]
> See the `ReadMe.md` file for `/Components/Inputs` for information regarding using parameters in `InputMain`