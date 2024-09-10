// function addText() {
//             // Get the value from the input box
//             const input = document.getElementById('textInput');
//             const text = input.value.trim();
            
//             // Check if input is not empty
//             if (text === '') {
//                 alert('Please enter some text.');
//                 return;
//             }

//             // Create a new div element for the list item
//             const listItem = document.createElement('div');
//             listItem.className = 'list-item';
//             listItem.textContent = text;

//             // Append the new item to the list
//             const textList = document.getElementById('textList');
//             textList.appendChild(listItem);

//             // Clear the input field
//             input.value = '';
//         }