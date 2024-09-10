Triswick Assessment Backend API

Overview
This project is the backend of a web forum using ASP.NET Web API (Yes I dont know why I just didnt use CORE) and SQL Server. The forum supports creating posts, commenting, liking posts, and moderating content.

Getting Started

Exernal Dependencies
- [.NET 6 SDK]
- [SQL Server]
- 

things of note: 
		---builder.Services.AddCors(options =>
		--{
		---//change to allow running locally
		---options.AddPolicy("AllowSpecificOrigins",
		--	-builder => builder
		--		-.WithOrigins("https://localhost:7197/", "http://127.0.0.1:8080")
		--		-.AllowAnyMethod()
		--		-.AllowAnyHeader());
		--});
		
		#If runing app locally replace these links with your machines endpoints.(I had some trouble getting the testing UI to connect,this solved that issue).

** If api respone is failing even after database-migration/update-database/rebuild - open SQLSMS and drop that databse (delete) it. (This should not be an issue as it only happened to me once)

LINK to postman docs - https://web.postman.co/workspace/My-Workspace~e67409f1-9737-4174-879f-939717da7bb6/documentation/23953045-3109d046-40dd-49fd-8ad6-cfed7dae1c5e
# I was going to merge the master andthe latest 3.1API Testing but I think it safer for now to leave it unmerged. as I am already thinking of improvements to the code.


Tests users : 	"Regular" user
				        Username = "regUser",
				Password = "Password123",
				UserRole = "Regular" UserModel
				
			  "Moderator" user
				Username = "modUser",
				Password = "ModPassword123", 
				UserRole = "Moderator"


DbSets:

DbSet<PostModel> Posts: Represents the table for posts in the forum.
DbSet<UserModel> Users: Represents the table for users.
DbSet<CommentsModel> Comments: Represents the table for comments on posts.
DbSet<LikesModel> Likes: Represents the table for likes on posts.
DbSet<TagModel> Tags: Represents the table for tags associated with posts.

[HttpGet] - GetAllPosts
Function: Retrieves all posts from the database, including associated comments and tags, sorted by the most recently updated. 

[HttpPost("login/{username}/{password}")] - Login
Function: Handles user login. Validates the provided username and password. If the credentials match, it signs the user in and returns an authentication token. seperates regular users and moderators

[HttpPost("register")] - Register
Function: Registers a new user. Checks if the username is already taken. If not, it creates a new user with the given details and stores them in the database.

[HttpPost] - CreatePost
Function: Creates a new post. Checks if a post with the same ID already exists. If not, it adds the post to the database with the current date attached.

[HttpPost("{postId}/comments")] - AddComment
Function: Adds a comment to a specified post. Checks if the post exists, then corresponding post and saves it to the database.

[HttpPost("{postId}/tags")] - AddTag
Function: Adds a tag to a specified post. Same as comment, finds the corresponding post.

[HttpPut("UpdateLikes/{id}")] - UpdateLikeCount
Function: Increments the like count for a specified post.

[HttpPut("Unlike/{id}")] - UpdateLikeCount
Function: decrements the like count for a specified post.

