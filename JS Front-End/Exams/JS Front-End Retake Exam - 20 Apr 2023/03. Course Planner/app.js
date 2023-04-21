const BASE_URL = "http://localhost:3030/jsonstore/tasks/";
var coursesList = document.getElementById("list");
var editCourseMain = document.getElementById("edit-course");
editCourseMain.addEventListener("click", editCourse);
var courseName = document.getElementById("course-name");
var courseType = document.getElementById("course-type");
var courseTeacher = document.getElementById("teacher-name");
var courseDescription = document.getElementById("description");

let button = document.getElementById("load-course");
button.addEventListener("click", listAll);

let addCourseBtn = document.getElementById("add-course");
addCourseBtn.addEventListener("click", addCourse);
function listAll(e) {
  if (e) {
    e.preventDefault();
  }
  courseName.value = "";
  courseType.value = "";
  courseDescription.value = "";
  courseTeacher.value = "";
  coursesList.innerHTML = "";

  fetch("http://localhost:3030/jsonstore/tasks/")
    .then((response) => response.json())
    .then((data) => {
      Object.values(data).forEach(
        ({ title, type, description, teacher, _id }) => {
          console.log(3);
          let container = createElement("div", "", coursesList, "container");

          let internalTitle = createElement(
            "h2",
            title,
            container,
            "course-name"
          );

          let internalTeacher = createElement(
            "h3",
            teacher,
            container,
            "course-teacher"
          );

          let internalType = createElement(
            "h3",
            type,
            container,
            "course-type"
          );

          let internalDescription = createElement(
            "h4",
            description,
            container,
            "course-desc"
          );

          let editBtn = createElement(
            "button",
            "Edit Course",
            container,
            "edit-btn"
          );
          editBtn.id = _id;
          editBtn.addEventListener("click", loadInput);

          let finishBtn = createElement(
            "button",
            "Finish Course",
            container,
            "finish-btn"
          );
          finishBtn.id = _id;
          finishBtn.addEventListener("click", finishCourse);
        }
      );
    });
  addCourseBtn.disabled = false;
  editCourseMain.disabled = true;
  return;
}
function addCourse(e) {
  e.preventDefault();

  fetch("http://localhost:3030/jsonstore/tasks/", {
    method: "POST",
    headers: {
      "Content-Type": "application/json",
    },
    body: JSON.stringify({
      title: courseName.value,
      type: courseType.value,
      description: courseDescription.value,
      teacher: courseTeacher.value,
    }),
  }).then((response) => {
    listAll(e);
  });
}

function loadInput(e) {
  e.preventDefault();

  let parent = e.target.parentElement;
  courseName.value = parent.querySelector(`.course-name`).textContent;
  courseType.value = parent.querySelector(`.course-type`).textContent;
  courseDescription.value = parent.querySelector(`.course-desc`).textContent;
  courseTeacher.value = parent.querySelector(`.course-teacher`).textContent;

  editCourseMain.disabled = false;
  editCourseMain.setAttribute("course-id", e.target.id);
  addCourseBtn.disabled = true;
  coursesList.removeChild(parent);
}
function editCourse(e) {
  e.preventDefault();
  const headers = {
    method: "PUT",
    body: JSON.stringify({
      title: courseName.value,
      type: courseType.value,
      description: courseDescription.value,
      teacher: courseTeacher.value,
    }),
  };
  fetch(
    `http://localhost:3030/jsonstore/tasks/${e.target.getAttribute(
      "course-id"
    )}`,
    headers
  )
    .then(() => {
      listAll(e);
    })
    .catch((err) => {
      console.log(err);
    });
}
function finishCourse(e) {
  let deleteId = e.target.id;
  const headers = {
    method: "DELETE",
  };
  const deleteURL = `${BASE_URL}${deleteId}`;
  fetch(deleteURL, headers).then(() => {
    listAll(e);
  });
}

function createElement(elementTag, value, parent, classNameInt) {
  const newElement = document.createElement(elementTag);
  newElement.innerHTML = value;
  if (parent) {
    parent.appendChild(newElement);
  }
  if (classNameInt) {
    newElement.className = classNameInt;
  }

  return newElement;
}
