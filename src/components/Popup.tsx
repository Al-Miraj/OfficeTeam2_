import React, { useState } from "react";

interface PopUpProps {
  isOpen: boolean;
  onClose: () => void;
}

const Popup: React.FC<PopUpProps> = ({ isOpen, onClose }) => {
  const [message, setMessage] = useState<string>("");

  const handleSubmit = async () => {
    // TODO: Send to backend
    setMessage("Email has been sent to reset password");
  };

  const handleClose = () => {
    setMessage("");
    onClose();
  };

  return (
    <div id="loginPopupContainer" style={{ display: isOpen ? "block" : "none" }}>
      <div id="loginPopUpBox">
        <h3>Please enter your email</h3>
        <div id="loginInputInputs">
          <div>
            <input type="email" placeholder="Email" required />
          </div>
          {message && <p>{message}</p>}
        </div>
        <div id="btnBox">
          <button id="closeBtn" onClick={handleClose}>Close</button>
          <button type="submit" id="resetBtn" onClick={handleSubmit}>Reset</button>
        </div>
      </div>
    </div>
  );
};

export default Popup;
