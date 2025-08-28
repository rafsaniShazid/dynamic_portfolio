<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="dynamic_portfolio.Default" %>
<!DOCTYPE html>
<html>
<head runat="server">
    <title>My Portfolio</title>
    <link href="Content/style.css" rel="stylesheet" />
    <script src="Scripts/script.js"></script>

    <!-- Scoped form styles -->
    <style>
        /* Contact form card */
        #contact .contact-form-card {
            max-width: 720px;
            margin: 32px auto 0;
            background: #fff;
            border-radius: 12px;
            box-shadow: 0 8px 24px rgba(0,0,0,0.08);
            padding: 24px;
        }
        #contact .contact-form-card h2 {
            margin: 0 0 16px;
        }

        /* Layout */
        #contact .form-grid {
            display: grid;
            grid-template-columns: 1fr;
            gap: 16px;
        }
        @media (min-width: 640px) {
            #contact .form-grid {
                grid-template-columns: 1fr 1fr;
            }
            #contact .form-group.col-span-2,
            #contact .form-actions.col-span-2 {
                grid-column: 1 / -1;
            }
        }

        /* Fields */
        #contact .form-group label {
            display: inline-block;
            margin-bottom: 6px;
            font-weight: 600;
        }
        #contact .input {
            width: 100%;
            box-sizing: border-box;
            padding: 12px 14px;
            border: 1px solid #d0d7de;
            border-radius: 8px;
            background: #fff;
            color: #0d1117;
            transition: border-color .15s ease, box-shadow .15s ease;
        }
        #contact .input:focus {
            outline: none;
            border-color: #fff5f5;
            box-shadow: 0 0 0 3px rgba(79,70,229,0.15);
        }
        #contact textarea.input {
            resize: vertical;
            min-height: 140px;
            line-height: 1.5;
        }

        /* Validation */
        #contact .validation-summary {
            margin: 0 0 8px;
            padding: 10px 12px;
            border-left: 4px solid #dc2626;
            background: #fff5f5;
            color: #991b1b;
            border-radius: 6px;
            display: none;
        }
        #contact .validator {
            display: block;
            margin-top: 6px;
            color: #dc2626;
            font-size: 0.9rem;
        }
        #contact .success {
            display: inline-block;
            margin-left: 12px;
            color: #166534;
        }
        #contact .error {
            display: inline-block;
            margin-left: 12px;
            color: #b91c1c;
        }

        /* Actions */
        #contact .form-actions {
            display: flex;
            align-items: center;
            gap: 12px;
            margin-top: 4px;
        }
        #contact .btn.btn-color-1 {
            background: #4f46e5;
            border: none;
            color: #fff;
            padding: 12px 18px;
            border-radius: 8px;
            cursor: pointer;
            transition: transform .06s ease, box-shadow .15s ease, background .15s ease;
        }
        #contact .btn.btn-color-1:hover {
            background: #4338ca;
            box-shadow: 0 6px 18px rgba(67,56,202,0.25);
        }
        #contact .btn.btn-color-1:active {
            transform: translateY(1px);
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" />
        <nav id="desktop-nav">
            <div class="logo">Md. Rafsani Shazid</div>
            <div>
                <ul class="nav-links">
                    <li><a href="#about">About</a></li>
                    <li><a href="#experience">Experience</a></li>
                    <li><a href="#projects">Projects</a></li>
                    <li><a href="#contact">Contact</a></li>
                </ul>
            </div>
        </nav>
        <nav id="hamburger-nav">
            <div class="logo">Md. Rafsani Shazid</div>
            <div class="hamburger-menu">
                <div class="hamburger-icon" onclick="toggleMenu()">
                    <span></span>
                    <span></span>
                    <span></span>
                </div>
                <!-- FIX: wrap li items inside a UL -->
                <ul class="menu-links">
                    <li><a href="#about" onclick="toggleMenu()">About</a></li>
                    <li><a href="#experience" onclick="toggleMenu()">Experience</a></li>
                    <li><a href="#projects" onclick="toggleMenu()">Projects</a></li>
                    <li><a href="#contact" onclick="toggleMenu()">Contact</a></li>
                </ul>
            </div>
        </nav>
        <section id="profile">
            <div class="section__pic-container">
                <img src="./assets/profile-pic.png" alt="Profile picture of Md. Rafsani Shazid">
            </div>
            <div class="section__text">
                <p class="section__text__p1">Hello, I'm</p>
                <h1 class="title">Md. Rafsani Shazid</h1>
                <p class="section__text__p2">
                    <span class="animated-text" id="role-text">Android Developer</span>
                </p>
                <div class="btn-container">
                    <button class="btn btn-color-1" onclick="window.open('./assets/resume.pdf')">
                        Download CV</button>
                    <button class="btn btn-color-2" onclick="location.href='./#contact'">
                        Contact Info</button>
                </div>
                <div id="social-container">
                    <img src="./assets/linkedin.png" alt="My linkedin profile" class="icon" onclick="location.href=
                    'https://www.linkedin.com/in/md-rafsani-shazid-393230289/'">
                    <img src="./assets/github.png" alt="My github profile" class="icon" onclick="location.href=
                    'https://github.com/rafsaniShazid'">
                </div>
            </div>
        </section>
        <section id="about">
            <p class="section__text__p1">Get to Know More</p>
            <h1 class="title">About Me</h1>
            <div class="section-container">
                <div class="section__pic-container">
                    <img src="./assets/about-pic.png" alt="profile picture" class="about-pic" />
                </div>
                <div class="about-details-container">
                    <div class="about-containers">
                        <div class="details-container">
                            <img src="./assets/experience.png" alt="experience-icon" class="icon" />
                            <h3>Experience</h3>
                            <p>2+ years in Development<br>
                                1+ year in SEO</p>
                        </div>
                        <div class="details-container">
                            <img src="./assets/education.png" alt="education-icon" class="icon" />
                            <h3>Education</h3>
                            <p>Bsc in computer science and engineering<br>
                            </p>
                        </div>
                    </div>
                    <div class="text-container">
                        <p>
                            I'm a CS student from Bangladesh with a passion for technology, problem-solving,
                            and lifelong learning. I specialize in C++ and have experience in mobile app and web
                            development.<br>
                            Currently, I’m diving into AI and Machine Learning, aiming to contribute to impactful research.
                            Outside of tech, I enjoy football, staying active, watching movies and connecting with friends.
                            I'm always eager to learn, collaborate, and take on new challenges.
                        </p>
                    </div>
                </div>
            </div>
            <img src="./assets/arrow.png" alt="arrow icon" class="icon arrow" onclick="location.href='./#experience'" />
        </section>
        <section id="experience">
            <p class="section__text__p1">Explore my</p>
            <h1 class="title">Experience</h1>
            <div class="experience-details-container">
                <div class="about-containers">
                    <div class="details-container">
                        <h2 class="experience-sub-title">Frontend Development</h2>
                        <div class="article-container">
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>HTML</h3>
                                    <p>Experienced</p>
                                </div>
                            </article>
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>CSS</h3>
                                    <p>Experienced</p>
                                </div>
                            </article>
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>Javascript</h3>
                                    <p>Basic</p>
                                </div>
                            </article>
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>XML</h3>
                                    <p>Basic</p>
                                </div>
                            </article>
                        </div>
                    </div>
                    <div class="details-container">
                        <h2 class="experience-sub-title">Backend Development</h2>
                        <div class="article-container">
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>PHP</h3>
                                    <p>Basic</p>
                                </div>
                            </article>
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>JAVA</h3>
                                    <p>Experienced</p>
                                </div>
                            </article>
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>ASP.NET</h3>
                                    <p>Basic</p>
                                </div>
                            </article>
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>C++</h3>
                                    <p>Basic</p>
                                </div>
                            </article>
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>Python</h3>
                                    <p>Basic</p>
                                </div>
                            </article>
                            <article>
                                <img src="./assets/checkmark.png" alt="Experience icon" class="icon" />
                                <div>
                                    <h3>GIT</h3>
                                    <p>Basic</p>
                                </div>
                            </article>
                        </div>
                    </div>
                </div>
            </div>
            <img src="./assets/arrow.png" alt="arrow icon" class="icon arrow" onclick="location.href='./#projects'" />
        </section>
        <section id="projects">
            <p class="section__text__p1">Browse my recent</p>
            <h1 class="title">Projects</h1>
            <div class="experience-details-container">
                <div class="about-containers">
                    <asp:Repeater ID="rptProjects" runat="server">
                        <ItemTemplate>
                            <div class="details-container color-container">
                                <div class="article-container">
                                    <img src="<%# ResolveUrl((string)Eval("ImagePath")) %>"
                                         alt="<%# Eval("Alt") %>"
                                         class="project-img <%# Eval("ImageCss") %>" />
                                    <h2 class="experience-subtitle project-title"><%# Eval("Title") %></h2>
                                    <div class="btn-container">
                                        <a class="btn btn-color-1 project-btn" href="<%# Eval("GithubUrl") %>">Github</a>
                                        <asp:HyperLink runat="server"
                                            CssClass="btn btn-color-1 project-btn"
                                            NavigateUrl='<%# Eval("LiveUrl") %>'
                                            Text="Live demo"
                                            Visible='<%# !string.IsNullOrEmpty((string)Eval("LiveUrl")) %>' />
                                    </div>
                                    <asp:Literal runat="server"
                                        Text='<%# Eval("Description") %>'
                                        Visible='<%# !string.IsNullOrEmpty((string)Eval("Description")) %>' />
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
            <img src="./assets/arrow.png" alt="arrow icon" class="icon arrow" onclick="location.href='#contact'" />
        </section>
        <section id="contact">
            <p class="section__text__p1">Get in touch</p>
            <h1 class="title">Contact Me</h1>

            <!-- existing contact links -->
            <div class="contact-info-upper-container">
                <div class="contact-info-container">
                    <img src="./assets/email.png" alt="Email icon" class="icon email-icon" />
                    <p><a href="mailto:rafsanishazid@gmail.com">
                            rafsanishazid@gmail.com</a></p>
                </div>
                <div class="contact-info-container">
                    <img src="./assets/linkedin.png" alt="Linkedin icon" class="icon contact-icon" />
                    <p><a href="https://www.linkedin.com/in/md-rafsani-shazid-393230289/">
                            LinkedIn</a></p>
                </div>
            </div>

            <!-- enhanced form card -->
            <div class="contact-form-card">
                <h2 class="experience-sub-title">Send a message</h2>

                <asp:ValidationSummary ID="valSummary" runat="server" CssClass="validation-summary" />

                <div class="form-grid">
                    <div class="form-group">
                        <label for="<%= txtName.ClientID %>">Name</label>
                        <asp:TextBox ID="txtName" runat="server" CssClass="input" placeholder="Your Name" />
                        <asp:RequiredFieldValidator ID="rfvName" runat="server"
                            ControlToValidate="txtName" ErrorMessage="Name is required."
                            Display="Dynamic" CssClass="validator" />
                    </div>

                    <div class="form-group">
                        <label for="<%= txtEmail.ClientID %>">Email</label>
                        <asp:TextBox ID="txtEmail" runat="server" CssClass="input" placeholder="your@email.com" />
                        <asp:RequiredFieldValidator ID="rfvEmail" runat="server"
                            ControlToValidate="txtEmail" ErrorMessage="Email is required."
                            Display="Dynamic" CssClass="validator" />
                        <asp:RegularExpressionValidator ID="revEmail" runat="server"
                            ControlToValidate="txtEmail" Display="Dynamic" CssClass="validator"
                            ErrorMessage="Enter a valid email."
                            ValidationExpression="^[^@\\s]+@[^@\\s]+\\.[^@\\s]+$" />
                    </div>

                    <div class="form-group col-span-2">
                        <label for="<%= txtMessage.ClientID %>">Message</label>
                        <asp:TextBox ID="txtMessage" runat="server" CssClass="input" TextMode="MultiLine"
                                     Rows="6" placeholder="How can I help?"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="rfvMessage" runat="server"
                            ControlToValidate="txtMessage" ErrorMessage="Message is required."
                            Display="Dynamic" CssClass="validator" />
                    </div>

                    <div class="form-actions col-span-2">
                        <asp:Button ID="btnSend" runat="server" Text="Send Message"
                            CssClass="btn btn-color-1" OnClick="btnSend_Click" />
                        <asp:Label ID="lblResult" runat="server" EnableViewState="false" />
                    </div>
                </div>
            </div>
        </section>
        <footer>
            <nav>
                <div class="nav-links-container">
                    <ul class="nav-links">
                        <li><a href="#about">About</a></li>
                        <li><a href="#experience">Experience</a></li>
                        <li><a href="#projects">Projects</a></li>
                        <li><a href="#contact">Contact</a></li>
                    </ul>
                </div>
            </nav>
            <p>Copyright &#169; 2025 Md. Rafsani Shazid.
                All Rights Reserved </p>
        </footer>
    </form>
</body>
</html>