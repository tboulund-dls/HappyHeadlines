import { useState, useEffect } from "react";
import Navbar from "./Components/Navbar";
import Articles from "./Components/Articles";
import SubscribeModal from "./Components/SubscribeModal";
import LoginModal from "./Components/LoginModal";
import { Box } from "@chakra-ui/react";
import { getArticles } from "./Services/ArticleService";

const App: React.FC = () => {
    const [isLoggedIn, setIsLoggedIn] = useState(false);
    const [userEmail, setUserEmail] = useState<string | null>(null);
    const [isSubscribeModalOpen, setIsSubscribeModalOpen] = useState(false);
    const [isLoginModalOpen, setIsLoginModalOpen] = useState(false);
    const [articles, setArticles] = useState([]);

    useEffect(() => {
        const fetchedArticles = getArticles();
        setArticles(fetchedArticles);
    }, []);

    const handleLogin = (email: string) => {
        setUserEmail(email);
        setIsLoggedIn(true);
        setIsLoginModalOpen(false);
    };

    const handleSubscribeClick = () => {
        if (!isLoggedIn) {
            setIsLoginModalOpen(true);
        } else {
            setIsSubscribeModalOpen(true);
        }
    };

    return (
        <Box width="100vw" height="100vh" bg="#EDD4B2">
            <Navbar
                isLoggedIn={isLoggedIn}
                onLoginClick={() => setIsLoginModalOpen(true)}
                onLogoutClick={() => {
                    setIsLoggedIn(false);
                    setUserEmail(null);
                }}
                onSubscribeClick={handleSubscribeClick}
            />
            <Articles articles={articles} />
            <LoginModal
                isOpen={isLoginModalOpen}
                onClose={() => setIsLoginModalOpen(false)}
                onLogin={handleLogin}
            />
            <SubscribeModal
                isOpen={isSubscribeModalOpen}
                onClose={() => setIsSubscribeModalOpen(false)}
                userEmail={userEmail!}
            />
        </Box>
    );
};

export default App;