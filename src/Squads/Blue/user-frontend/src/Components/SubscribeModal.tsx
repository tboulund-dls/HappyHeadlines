import {
    Modal,
    ModalOverlay,
    ModalContent,
    ModalHeader,
    ModalFooter,
    ModalBody,
    Button,
    Checkbox,
    Stack,
    Flex,
} from "@chakra-ui/react";
import { useState } from "react";
import { createSubscription } from "../Services/SubscriptionService";

interface SubscribeModalProps {
    isOpen: boolean;
    onClose: () => void;
    userEmail: string;
}

const SubscribeModal: React.FC<SubscribeModalProps> = ({ isOpen, onClose, userEmail }) => {
    const [selectedTypes, setSelectedTypes] = useState<string[]>([]);

    const handleToggle = (type: string) => {
        setSelectedTypes((prev) =>
            prev.includes(type) ? prev.filter((t) => t !== type) : [...prev, type]
        );
    };

    const handleSubscribe = async () => {
        for (const type of selectedTypes) {
            await createSubscription(userEmail, type);
        }
        setSelectedTypes([]);
        onClose();
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
                bg="#CAC2B5" // Modal background color
                borderRadius="lg"
                boxShadow="lg"
                color="black" // Text color
                zIndex="1500"
                pointerEvents="auto" // Allow interaction with modal content
            >
                <Flex direction="column" align="center" gap={6} p={4}>
                    <ModalHeader textAlign="center" fontSize="2xl">
                        Subscribe
                    </ModalHeader>
                    <ModalBody width="100%" display="flex" justifyContent="center">
                        <Stack spacing={3} align="center">
                            <Checkbox onChange={() => handleToggle("DAILY")}>Daily</Checkbox>
                            <Checkbox onChange={() => handleToggle("NEWSSTREAM")}>News Stream</Checkbox>
                        </Stack>
                    </ModalBody>
                    <ModalFooter width="100%">
                        <Flex justify="space-between" width="100%">
                            <Button colorScheme="gray" onClick={onClose}>
                                Close
                            </Button>
                            <Button colorScheme="teal" onClick={handleSubscribe}>
                                Subscribe
                            </Button>
                        </Flex>
                    </ModalFooter>
                </Flex>
            </ModalContent>
        </Modal>
    );
};

export default SubscribeModal;