Code Readme

Just in case the strings from SelectByAttributesForm.resx are deleted, there is a copy saved.

SingletonsList.cs

This class contains list of items frequently used by the Select By Attributes form. Declared the class static so that we need not initialize each time to access it's variables. For now, all the objects are being opened in the 'write' mode.



There is SelectClass.cs which 

1. Checks if there are any entities in the dwg, if none, it initializes the SelectByAttributesForm with no parameters passed. If no parameters are passed, everything in the UI is disabled. This is true, if there are entities but no feature classes in the dwg.

2. If there are entities with feature classes, 3 parameters are passed:
	a. bool value indicating if invoked by TableViewer - only to 		   make it easy for adding code later.
	b. Current feature class name
	c. List of feature class names






SelectByAttributesForm.cs

1. When the three parameters are passed, first initialize the resource manager.

2. Update the methods combo box.

3. Update feature class combo box.

4. Once the feature class combo box is updated, there is a Index changed event, which updates DataTable. 

5. I am updating the DataTable everytime the feature class is changed.
I was worried about the performance, I checked it on a drawing with 50k entities and it worked fine.

6. Also, a static list of oids is maintained.

Please let me know if more information is needed.
