import React, { FormEvent, useState } from "react";
import Popup from "./Popup";
import "./Login.css";
import { useNavigate } from "react-router-dom";
import { User } from "../Models/User";
import users from "../../Data/Users.json";


const Login = () => {
    const [email, setEmail] = useState("");
    const [password, setPassword] = useState("");
    const [isPopupVisible, setIsPopupVisible] = useState(false);
    const navigate = useNavigate();

    const handleOpenPopUp = () => setIsPopupVisible(true);
    const handleClosePopUp = () => setIsPopupVisible(false);

    const handleSubmit = (event: React.FormEvent<HTMLFormElement>) => {
        event.preventDefault();
    
        console.log("Form submitted");
        console.log("Email:", email, "Password:", password);
    
        
        
        const user = users.find((user: User) => user.Email === email && user.Password === password);

        
    
        if (user) {
            console.log("Login successful");
            localStorage.setItem("isAuthenticated", "true"); 
            localStorage.setItem("userId", user.Id);
            navigate("/calendar");
        } else {
            console.log("Invalid credentials");
            alert("Invalid credentials!");
        }
    };

    return (
        <div id="login_page">
            <div id="loginContainer">
                <div id="loginLogform">
                    <div id="loginInputHeader">
                        <div>Log In</div>
                    </div>
                    <form onSubmit={handleSubmit} id="login-form">
                        <div id="loginInputInputs">
                            <div>
                                <input
                                    type="email"
                                    placeholder="Email"
                                    value={email}
                                    onChange={(e) => setEmail(e.target.value)}
                                    required
                                />
                            </div>
                            <div>
                                <input
                                    type="password"
                                    placeholder="Password"
                                    value={password}
                                    onChange={(e) => setPassword(e.target.value)}
                                    required
                                />
                            </div>
                        </div>
                        <div id="loginFoot">
                            <div id="loginForgotPassword">
                                Lost Password?<span onClick={handleOpenPopUp}>Click Here!</span>
                            </div>
                            <div id="loginSubmitContainer">
                                <button type="submit" id="loginSubmit">Log In</button>
                            </div>
                        </div>
                    </form>
                </div>
                <Popup isOpen={isPopupVisible} onClose={handleClosePopUp} />
            </div>
        </div>
    );
};

export default Login;
