//NOTE: When running the api locally change this to your localhost - yls
const uri = 'https://localhost:7197/api/Auth';

// moderator
// modpassword


async function login() {
    // Get the username and password values
    const username = document.getElementById('username').value;
    const password = document.getElementById('password').value;
    const errorMessage = document.getElementById('error-message');

    if (!username || !password) {
        errorMessage.textContent = 'Username and password are required.';
        return;
    }

    try {
        // Send login request to the API
        const response = await fetch(uri + '/login/' + encodeURIComponent(username) + '/' + encodeURIComponent(password), {
            method: 'POST',
            headers: {
                'Content-Type': 'application/json'
            }
        });

        const result = await response.json();

        if (response.ok) {
            // Redirect
            window.location.href = 'post.html';
        } else {
            errorMessage.textContent = result.message || 'Invalid username or password.';
        }
    } catch (error) {
        console.error('Error during login:', error);
        errorMessage.textContent = 'An error occurred. Please try again later.';
    }
}
