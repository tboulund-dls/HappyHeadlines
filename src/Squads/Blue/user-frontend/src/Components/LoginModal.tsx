import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    Button,
    Input,
    Flex,
} from "@chakra-ui/react";
import { useState, useRef, useEffect } from "react";

interface LoginModalProps {
    isOpen: boolean;
    onClose: () => void;
    onLogin: (email: string) => void;
}

const LoginModal: React.FC<LoginModalProps> = ({ isOpen, onClose, onLogin }) => {
    const [email, setEmail] = useState("");
    const inputRef = useRef<HTMLInputElement>(null);

    useEffect(() => {
        if (isOpen && inputRef.current) {
            inputRef.current.focus(); // Automatically focus the input when modal opens
        }
    }, [isOpen]);

    const handleLogin = () => {
        if (email.trim()) {
            onLogin(email);
            setEmail(""); // Clear the input field after login
            onClose(); // Close the modal
        }
    };

    return (
        <Modal isOpen={isOpen} onClose={onClose}>
            {/* Full-Screen Overlay */}
            <ModalOverlay
                bg="rgba(0, 0, 0, 0.5)" // Semi-transparent black overlay
                position="fixed"
                zIndex="1000"
                pointerEvents="none" // Prevent interaction with overlay
            />

            {/* Modal Content */}
            <ModalContent
                position="fixed"
                top="50%"
                left="50%"
                transform="translate(-50%, -50%)"
                maxW="400px"
                bg="#CAC2B5"
                borderRadius="lg"
                boxShadow="lg"
                color="black"
                zIndex="1500"
                pointerEvents="auto" // Allow interaction with modal content
            >
                <Flex direction="column" align="center" gap={6} p={4}>
                    <ModalHeader textAlign="center" fontSize="2xl">
                        Log In
                    </ModalHeader>
                    <ModalBody width="100%" display="flex" justifyContent="center">
                        <Input
                            ref={inputRef}
                            placeholder="Enter your email"
                            value={email}
                            onChange={(e) => setEmail(e.target.value)}
                            size="lg"
                            textAlign="center"
                            bg="gray.100"
                            borderRadius="md"
                            width="100%"
                        />
                    </ModalBody>
                    <ModalFooter width="100%">
                        <Flex justify="space-between" width="100%">
                            <Button colorScheme="gray" onClick={onClose}>
                                Close
                            </Button>
                            <Button colorScheme="teal" onClick={handleLogin}>
                                Log In
                            </Button>
                        </Flex>
                    </ModalFooter>
                </Flex>
            </ModalContent>
        </Modal>
    );
};

export default LoginModal;