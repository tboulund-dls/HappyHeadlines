import { Button, Flex, Text, Box } from "@chakra-ui/react";

interface NavbarProps {
    isLoggedIn: boolean;
    onLoginClick: () => void;
    onLogoutClick: () => void;
    onSubscribeClick: () => void;
}

const Navbar: React.FC<NavbarProps> = ({
                                           isLoggedIn,
                                           onLoginClick,
                                           onLogoutClick,
                                           onSubscribeClick,
                                       }) => {
    return (
        <Flex
            justify="space-between"
            align="center"
            p={4}
            bg="#4D243D"
            color="white"
            position="fixed"
            top={0}
            left={0}
            width="100%"
            height="60px"
            zIndex={1000}
        >
            {/* Left Button */}
            <Button
                onClick={isLoggedIn ? onLogoutClick : onLoginClick}
                bg="transparent"
                color="white"
                _hover={{ bg: "rgba(255, 255, 255, 0.1)" }}
            >
                {isLoggedIn ? "Log Out" : "Log In"}
            </Button>

            {/* Centered Text */}
            <Box position="absolute" left="50%" transform="translateX(-50%)">
                <Text fontSize="lg" fontWeight="bold" color="white">
                    Happy Headlines
                </Text>
            </Box>

            {/* Right Button */}
            {isLoggedIn && (
                <Button
                    onClick={onSubscribeClick}
                    bg="transparent"
                    color="white"
                    _hover={{ bg: "rgba(255, 255, 255, 0.1)" }}
                >
                    Subscribe
                </Button>
            )}
        </Flex>
    );
};

export default Navbar;