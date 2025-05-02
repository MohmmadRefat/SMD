function renderDashboard() {
    // Main container for the app
    const app = document.createElement('div');
 
    // ===== Header Section =====
    const header = document.createElement('div');
    header.className = 'header';
 
    const h1 = document.createElement('h1');
    h1.textContent = 'Dashboard';
 
    const actions = document.createElement('div');
    actions.className = 'actions';
 
    const btnStudent = document.createElement('button');
    btnStudent.className = 'btn dash-btn blue';
    btnStudent.innerHTML = '<i class="fa-solid fa-user-group"></i> <span>Add Student</span>';
 
    const btnCourse = document.createElement('button');
    btnCourse.className = 'btn dash-btn green';
    btnCourse.innerHTML = '<i class="fa-solid fa-book"></i> <span>Add Course</span>';
 
    actions.appendChild(btnStudent);
    actions.appendChild(btnCourse);
    header.appendChild(h1);
    header.appendChild(actions);
 
    // ===== Summary Cards Section =====
    const summary = document.createElement('div');
    summary.className = 'summary-cards';
 
    const cards = [
       {
          title: 'Total Students',
          value: '2',
          icon: 'fa-user-group',
       },
       {
          title: 'Total Courses',
          value: '2',
          icon: 'fa-book',
       },
       {
          title: 'Departments',
          value: '1',
          icon: 'fa-building',
       },
       {
          title: 'Average GPA',
          value: '3.74 <span class="gpa-state"> Good</span>',
          icon: 'fa-chart-line',
       },
    ];
 
    cards.forEach((card) => {
       const cardDiv = document.createElement('div');
       cardDiv.className = 'card';
 
       const infoDiv = document.createElement('div');
       infoDiv.innerHTML = '<p>${card.title}</p><p>${card.value}</p>';
 
       const iconDiv = document.createElement('div');
       iconDiv.className = 'icon';
       iconDiv.innerHTML = `<i class="fa-solid ${card.icon}"></i>`;
 
       cardDiv.appendChild(infoDiv);
       cardDiv.appendChild(iconDiv);
       summary.appendChild(cardDiv);
    });
 
    // ===== Add Everything to Page =====
    app.appendChild(header);
    app.appendChild(summary);
 
    // ===== Navigation add button action =====
    function removeActiveClass(btn, className) {
       setTimeout(() => {
          btn.classList.remove(className);
       }, 300);
    }
 
    function addAction(btn) {
       btn.addEventListener('click', _ => {
          btn.classList.add("btn-clicked");
          removeActiveClass(btn, "btn-clicked");
       })
    }
    addAction(btnStudent);
    addAction(btnCourse);
 
    return app;
 }
 
 // Function to handle navigation
 function navigateTo(pageId) {
    // Hide all pages
    document.querySelectorAll(".page").forEach(page => {
       page.classList.remove("render");
    });
 
    // Show the selected page
    const selectedPage = document.getElementById(pageId);
    if (selectedPage) {
       selectedPage.classList.add("render");
    }
 }
 
 // Event listener for navigation links
 const navLinks = document.querySelectorAll(".nav-link");
 navLinks.forEach(link => {
    link.addEventListener("click", (event) => {
       handelNavigationLinkClick(link);
       const pageId = link.getAttribute("data-page");
       renderProberPage(pageId); // Call the function to render the selected page
    });
 });
 
 function renderProberPage(pageId) {
    // Update the history state and URL
    history.pushState({ page: pageId }, "", "#",`${pageId}`);
 
    // Navigate to the selected page
    navigateTo(pageId);
 }
 
 function initPageLoad() {
    function isNavLink(link) {
       return link == "dashboard" || link == "students" || link == "courses";
    }
    // Handle back/forward navigation
    window.addEventListener("popstate", (event) => {
       const state = event.state;
       if (state && state.page) {
          navigateTo(state.page);
          if (isNavLink(state.page)) {
             handelNavigationLinkClick(document.querySelector(`[data-page="${state.page}"]`));
          }
       }
       else {
          navigateTo("dashboard");
          handelNavigationLinkClick(document.querySelector('[data-page="dashboard"]'));
       }
    });
    // On initial load, navigate based on the current URL
    const initialPage = location.hash.replace("#", "") || "dashboard";
    navigateTo(initialPage);
 
    handelNavigationLinkClick(document.querySelector(`[data-page="${initialPage}"]`));
 } initPageLoad();
 
 // ===== Navigation add button action =====
 document.querySelectorAll(".dash-btn").forEach(btn => {
 
    btn.addEventListener('click', _ => {
       // navigate to the page when the button is clicked
       const pageId = btn.getAttribute("data-page");
 
       // Remove active class from all buttons
       document.querySelectorAll(".nav-link").forEach(btn => {
          btn.classList.remove("active");
       });
 
       renderProberPage(pageId);
    })
 });
 
 function handelNavigationLinkClick(param) {
    // Remove active class from all buttons
    document.querySelectorAll(".nav-link").forEach(btn => {
       btn.classList.remove("active");
    });
    // Add active class to the clicked button
    param.classList.add("active");
 }
 
 
 function cancelElshogle() {
    document.getElementById("full-name").value
    document.getElementById("full-name").value
 
    if (isEmpty) { 
       navigateTo('dashboard');
    }
    else { 
       showCancelAlert();
    }
 }
 
 function showCancelAlert() {
    Swal.fire({
       title: "Are you sure you want to leave?",
       text: "If you leave now, your changes will not be saved.",
       icon: "warning",
       showCancelButton: true,
       confirmButtonColor: "#3085d6",
       cancelButtonColor: "#d33",
       confirmButtonText: "Yes, leave",
       cancelButtonText: "Stay on this page"
    }).then((result) => {
       if (result.isConfirmed) {
          navigateTo("dashboard");
       }
    });
 }
 
 function successSave() {
    Swal.fire({
       title: "Saved",
       icon: "success",
    }).then(() => {
       navigateTo("dashboard");
    });
 }
 
 