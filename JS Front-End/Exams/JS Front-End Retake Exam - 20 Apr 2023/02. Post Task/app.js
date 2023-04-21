window.addEventListener("load", solve);

function solve() {
  var publishBtn = document.getElementById("publish-btn");
  var list = document.getElementById("review-list");
  var publishedList = document.getElementById("published-list");
  publishBtn.addEventListener("click", publish);

  var taskTitle = document.getElementById("task-title");
  var taskCategory = document.getElementById("task-category");
  var taskContent = document.getElementById("task-content");

  function publish(e) {
    e.preventDefault();  

    if(taskTitle.value == "" || taskCategory.value == "" || taskContent.value == "") {
        return;
    }

    let li = document.createElement("li");
    li.classList.add("rpost");

    let article = document.createElement("article");
    let h4 = document.createElement("h4");
    h4.innerHTML = taskTitle.value;
    
    let cat = document.createElement("p");
    cat.innerHTML = `Category: ${taskCategory.value}`;

    let cont = document.createElement("p");
    cont.innerHTML = `Content: ${taskContent.value}`;

    article.appendChild(h4);
    article.appendChild(cat);
    article.appendChild(cont);

    let editBtn = document.createElement("button");
    editBtn.className = "action-btn edit";
    editBtn.textContent = "Edit";
    editBtn.addEventListener("click", edit);

    let postBtn = document.createElement("button");
    postBtn.className = "action-btn post";
    postBtn.textContent = "Post";
    postBtn.addEventListener("click", post);

    li.appendChild(article);
    li.appendChild(editBtn);
    li.appendChild(postBtn);

    list.appendChild(li);

    taskTitle.value = "";
    taskCategory.value = "";
    taskContent.value = "";
  }

  function edit(e) {
    e.preventDefault();

    let parent = e.target.parentElement;
    let editTitle = parent.children[0].children[0];
    let editCategory = parent.children[0].children[1];
    let editContent = parent.children[0].children[2];

    taskTitle.value = editTitle.innerHTML;
    taskCategory.value = editCategory.innerHTML.split(": ")[1];
    taskContent.value = editContent.innerHTML.split(": ")[1];

    list.removeChild(parent);
  }

  function post(e) {
    e.preventDefault();

    let li = e.target.parentElement;
    list.removeChild(li);

    li.removeChild(li.lastChild);
    li.removeChild(li.lastChild);
    publishedList.appendChild(li);
  }
}