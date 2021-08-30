# Itransition course project

 [![GitHub stars](https://img.shields.io/github/stars/PavelAndreyev1337/itransition-course-project)](https://github.com/PavelAndreyev1337/itransition-course-project/stargazers) [![GitHub forks](https://img.shields.io/github/forks/PavelAndreyev1337/itransition-course-project)](https://github.com/PavelAndreyev1337/itransition-course-project/network) [![GitHub issues](https://img.shields.io/github/issues/PavelAndreyev1337/itransition-course-project)](https://github.com/PavelAndreyev1337/itransition-course-project/issues) [![GitHub license](https://img.shields.io/github/license/PavelAndreyev1337/itransition-course-project)](https://github.com/PavelAndreyev1337/itransition-course-project/blob/master/LICENSE)


## How run ❓
* change database connection string 
* set secrets: **Authentication:Reddit:ClientId**, **Authentication:Facebook:AppSecret**, **Authentication:Facebook:AppId** **Authentication:Reddit:ClientSecret**, **Cloudinary:Name**, **Cloudinary:Key**, **Cloudinary:Secret**
* iinstall dependencies

## Screenshots 🖼️

![home](https://www.dropbox.com/s/b1w1rzmclq04dwd/home.png?dl=0)
![create](https://www.dropbox.com/s/awc4isl5ujs9rkm/create.png?dl=0)
![collections](https://www.dropbox.com/s/bnh8eh384ooxoju/collections.png?dl=0)
![create item](https://www.dropbox.com/s/l4rvyrmfeg444yr/create_item.png?dl=0)
![items](https://www.dropbox.com/s/jg8twhl5jm6ooec/items.png?dl=0)
![item](https://www.dropbox.com/s/i2qg8ett3ovkopt/item.png?dl=0)
![admin](https://www.dropbox.com/s/38dkownadh1xuu8/admin.png?dl=0)

## Tasks 📝

It is required to develop a website for managing personal collections (books, stamps, badges, whiskey, etc. - hereinafter in the text, what the collection consists of is called items).
Only read-only mode is available to unauthenticated users (search is available, creation of collections and items is not available, comments and likes are not available).
Authenticated users have access to everything except the admin area.
The admin panel allows you to manage users (view, block, delete, assign others as admins). The administrator sees each user page and each collection as its creator-owner (for example, he can edit or create a new collection on behalf of the user from his page, or add an item, etc.). Explanation: there is no need to make separate views for the admin (except for the "admin panel", of course).
Only the owner or admin can manage the collection (edit / add / delete).
It is required to support login via social networks (at least two).
On each page, a full-text search on the site is available (search results are always items, for example, if the text is found in the description of the collection or comments, which should be possible, then a link to the item is displayed). Display for search results - items.
Each user has his personal page, on which he manages the list of his collections (you can add, delete or edit) and from which you can go to the collection page (there is a table with filters and sorts, the ability to create / delete / edit an item).
Each collection consists of: a name, a short description of the collection with support for markdown formatting, a "theme" (from a fixed set, for example, "Alcohol", "Books", etc.), an optional collection image (stored in the cloud, loaded by drag-n- drop). In addition, the collection has the ability to specify the fields that each item will have in it (there are 3 fixed fields - id (generated automatically), name and tags, you can "include" some of the following - three numeric fields, three one-line fields, three text fields with markdown formatting, three dates, three boolean checkboxes). For example, you can specify that in my collection of books each item has (in addition to id, title and tags) a string field "Author", a text field "Comment", a date field "Year of publication". The text field is a field with markdown formatting. The fields are characterized by a name (when creating / editing a collection, the names of the fields are specified). Fields are displayed in the list of items - the list requires support for switchable sorts and filters. Explanation: all items in the database have all possible fields at once (for example, integer1), the collection has fields that specify which fields are displayed (for example, integer1_visible).
Each item has a field "tags" (this is one field, several tags are entered, autocompletion is necessary - when the user starts entering a tag, a list with variants of words that have already been entered earlier on the site] drops out.
The main page displays: the last added items, collections with the largest number of items, a tag cloud (when clicked, the result is a list of links to items, similar to search results, in fact it can be one view).
When an item is opened in Reading Mode by the author or simply opened by other users, comments are displayed at the end. Comments are linear, you cannot comment on a comment, a new one is added only "in the tail". It is necessary to implement automatic loading of comments - if I have a page with comments open and someone else adds a new one, it automatically appears for me (there may be a delay of 2-5 seconds).
The item must have likes (no more than one from one user per item).
The site must support two languages: Russian and English (the user selects and the choice is saved). The site must support two designs (themes): light and dark (the user selects and the choice is saved).
